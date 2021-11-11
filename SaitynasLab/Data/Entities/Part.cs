using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynasLab.Data.Entities
{
    public class Part
    {
        public int Id { get; set; }
        public string Instrument { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
    }
}
