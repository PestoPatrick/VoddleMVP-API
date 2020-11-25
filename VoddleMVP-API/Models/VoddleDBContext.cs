using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VoddleMVP_API.Models
{
    public class VoddleDBContext : DbContext
    {
        public VoddleDBContext(DbContextOptions<VoddleDBContext> options) : base(options)
        {
        }

        public DbSet<Video> Videos { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Video>().ToTable("tblVideos");
            modelBuilder.Entity<User>().ToTable("tblUsers");
        }
    }
}
