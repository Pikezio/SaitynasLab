using Microsoft.AspNetCore.Identity;
using SaitynasLab.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynasLab.Data.Entities
{
    public class Concert : IUserOwnedResource
    {
        public Concert()
        {
            Songs = new List<Song>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreationDate { get; set; }

        public string UserId { get; set; }
        //public IdentityUser User { get; set; }

        // Concert has many Songs
        public ICollection<Song> Songs { get; set; }
    }
}
