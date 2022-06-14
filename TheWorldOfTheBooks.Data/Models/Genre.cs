﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TheWorldOfTheBooks.Data.Models
{
    internal class Genre: BaseModel
    {
        public Genre()
        {
            this.Id = Guid.NewGuid();
            this.Books = new HashSet<Books>();
        }
        [Required]
        public string Title { get; set; }
        public ICollection<Books> Books { get; set; }
    }
}
