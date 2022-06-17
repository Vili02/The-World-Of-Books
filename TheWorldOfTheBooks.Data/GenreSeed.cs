using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorldOfTheBooks.Data.Models;

namespace TheWorldOfTheBooks.Data
{
    public class GenreSeed
    {
        public static void Seed(TheWorldOfBooksContext context)
        {
            if (context.Genres.Any())
            {
                return;
            }
            context.Genres.AddRange(
                new Genre
                {
                    Id = new Guid(),
                    Title = "Literaly fiction"
                },
                new Genre
                {
                    Id = new Guid(),
                    Title = "Mystery"
                },
                new Genre
                {
                Id = new Guid(),
                    Title = "Thriller"
                },
                new Genre
                {
                    Id = new Guid(),
                    Title = "Horror"
                },
                new Genre
                {
                    Id = new Guid(),
                    Title = "Historical"
                },
                new Genre
                {
                    Id = new Guid(),
                    Title = "Romance"
                },
                new Genre
                {
                    Id = new Guid(),
                    Title = "Fantasy"
                },
                new Genre
                {
                    Id = new Guid(),
                    Title = "Science fiction"
                });

            context.SaveChanges();
        }
    }
}