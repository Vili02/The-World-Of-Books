using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TheWorldOfTheBooks.Data.Models
{
    public class Genre: BaseModel
    {
        public Genre()
        {
            this.Id = Guid.NewGuid();
            this.Books = new HashSet<Book>();
        }
        [Required]
        public string Title { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
