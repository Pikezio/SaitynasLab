using Microsoft.EntityFrameworkCore;
using SaitynasLab.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AutoMapper.Configuration;
using SaitynasLab.Auth;

namespace SaitynasLab.Data
{
    public class RestContext : IdentityDbContext
    {
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Part> Parts { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public RestContext(DbContextOptions<RestContext> options) : base(options)
        {

        }
    }
}
