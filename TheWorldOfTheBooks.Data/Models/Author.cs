using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TheWorldOfTheBooks.Data.Models
{
    public class Author : BaseModel
    {
        public Author()
        {
            this.Books = new HashSet<Book>();
        }
        [Required]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
