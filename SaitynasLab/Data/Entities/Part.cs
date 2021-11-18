using SaitynasLab.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynasLab.Data.Entities
{
    public class Part : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Instrument { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        // Part belongs to a song
        public int SongId { get; set; }
        public Song Song { get; set; }

        public string UserId { get; set; }
    }
}
