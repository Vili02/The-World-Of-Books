using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheWorldOfTheBooks.Data.Models;

namespace TheWorldOfTheBooks.Data
{
    public class TheWorldOfBooksContext : IdentityDbContext<User>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public override DbSet<User> Users { get; set; }
        public TheWorldOfBooksContext(DbContextOptions<TheWorldOfBooksContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
