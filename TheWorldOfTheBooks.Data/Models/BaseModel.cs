﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorldOfTheBooks.Data.Models
{
    public abstract class BaseModel
    {
        public BaseModel()
        {
        }
        public Guid Id { get; set; }
    }
}
