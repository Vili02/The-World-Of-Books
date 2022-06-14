using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TheWorldOfTheBooks.Data.Models
{
    internal class Author : BaseModel
    {
        public Author()
        {
            this.Books = new HashSet<Books>();
        }
        [Required]
        public string Name { get; set; }
        public ICollection<Books> Books { get; set; }
    }
}
