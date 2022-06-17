using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheWorldOfTheBooks.Data;
using TheWorldOfTheBooks.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace TheWorldOfBooks.Web.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly TheWorldOfBooksContext _context;
        public BooksController(TheWorldOfBooksContext context)
        {
            _context = context;

        }

        [AllowAnonymous]
        public IActionResult Index(string? errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
            var TheWorldOfBooksContext = _context.Books.Include(c => c.Genre);
            return View(TheWorldOfBooksContext.ToList());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id, string? errorMessage)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception("Id not Defined!");
                }
                var book = await _context.Books
                    .Include(c => c.Genre)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (book == null)
                {
                    throw new Exception("Book Not Found!");
                }
                return View(book);
            }
            catch (Exception exc)
            {
                return RedirectToAction(nameof(this.Index), new { errorMessage = exc.Message });
            }
        }

        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,ImageURL,Published,Pages,GenreId,AuthorName")] Book book)
        {
            book.Id = new Guid();
            _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Title", book.GenreId);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book)
        {

            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(c => c.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(Guid id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}