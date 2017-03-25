using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace PracaInz.Models
{
    public class Opinion
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string UserId { get; set; }
        public string Opinia { get; set; }
        public int Ocena { get; set; }
        public virtual Car Car { get; set; }
        public virtual ApplicationUser User { get; set; }


    }
}