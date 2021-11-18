using Microsoft.AspNetCore.Identity;
using SaitynasLab.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SaitynasLab.Data.Entities
{
    public class Song : IUserOwnedResource
    {
        public Song()
        {
            Concerts = new List<Concert>();
            //Parts = new List<Part>();

            Arranger = "";
            Composer = "";
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Composer { get; set; }
        public string Arranger { get; set; }

        // Song has many parts
        //public List<Part> Parts { get; set; }

        // Song belongs to many Concerts
        public ICollection<Concert> Concerts { get; set; }

        public string UserId { get; set; }
    }
}
