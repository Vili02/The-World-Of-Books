using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TheWorldOfTheBooks.Data.Models
{
    internal class Books : BaseModel
    {
        public Books()
        {
        }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageURL { get; set; }
        [Required]
        public string Published { get; set; }
        [Required]
        public string Pages { get; set; }
        [Required]
        public int PageCount { get; set; }
        [Required]
        public Guid GenreId { get; set; }
        public Guid AthorId { get; set; }
        public Genre Genre { get; set; }
        public Author Author { get; set; }
    }
}
