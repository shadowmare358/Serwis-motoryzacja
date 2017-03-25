using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PracaInz.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string Model { get; set; }
        public string Nadwozie { get; set; }
        public string Segment { get; set; }
        public string Pojemnosc { get; set; }
        public string Opis { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Opinion> Opinions { get; set; }

    }
}


