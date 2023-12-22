using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Book
    {
        public int? IdBook { get; set; }

        public string? BookName { get; set; }

        public string? Author { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public List<object>? Bookss { get; set; }
    }
}
