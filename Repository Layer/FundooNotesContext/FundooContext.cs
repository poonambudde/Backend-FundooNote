using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Repository_Layer.Entity;

namespace Repository_Layer.FundooNotesContext
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Entity.User> Users { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<Label> Label{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entity.User>()
            .HasIndex(u => u.email)
            .IsUnique();

        }
    }
}
