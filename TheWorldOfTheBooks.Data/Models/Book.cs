using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TheWorldOfTheBooks.Data.Models
{
    public class Book : BaseModel
    {
        public Book()
        {
        }
        [Required]
        public string Title { get; set; }
        [Required]
        [MinLength(50)]
        public string Description { get; set; }
        [Required]
        public string ImageURL { get; set; }
        [Required]
        public string Published { get; set; }
        [Required]
        public string Pages { get; set; }
        [Required]
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
        public string AuthorName { get; set; }
    }
}
