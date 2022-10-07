using System;
using System.Collections.Generic;
using System.Text;

namespace layihe1.Models
{
    public class BaseModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public bool SoftDelete { get; set; }

    }
}
