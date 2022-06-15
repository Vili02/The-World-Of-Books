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
    public class BookController : Controller
    {
        private readonly TheWorldOfBooksContext _context;
        public BookController(TheWorldOfBooksContext context)
        {
            _context = context;

        }

        // GET: Books
        public async Task<IActionResult> Index(string? errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
            var TheWorldOfBooksContext = _context.Books.Include(c => c.Genre);
            return View(await TheWorldOfBooksContext.ToListAsync());
        }

        // GET: Books/Details/5
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

        // GET: Books/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Title");
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")] // allows only admins to create books
        public async Task<IActionResult> Create([Bind("Title,Description,ImageURL,Published,Pages,PageCount,GenreId,AuthorId")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.Id = Guid.NewGuid();
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id,Title", book.GenreId.ToString()) ;
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize]
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

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Description,ImageURL,Published,Pages,PageCount,GenreId,AuthorId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Title", book.GenreId);
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize]
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

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
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