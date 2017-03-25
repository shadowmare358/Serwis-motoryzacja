using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracaInz.Models
{
  
    public class Average
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int Srednia { get; set; }
        public virtual Car Car { get; set; }
    }
}
