using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynasLab.Data.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Composer { get; set; }
        public string Arranger { get; set; }
        public int ConcertId { get; set; }
        public Concert Concert { get; set; }
    }
}
