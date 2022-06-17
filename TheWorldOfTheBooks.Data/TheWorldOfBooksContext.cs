
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheWorldOfTheBooks.Data.Models;

namespace TheWorldOfTheBooks.Data
{
    public class TheWorldOfBooksContext : IdentityDbContext<User>
    {
        public TheWorldOfBooksContext(DbContextOptions<TheWorldOfBooksContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public override DbSet<User> Users { get; set; }
       
    }
}
