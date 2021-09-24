using Gymbokning.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gymbokning.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GymClass> GymClasses { get; set; }
        public DbSet<ApplicationUserGymclass> ApplicationUserGymclasses { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserGymclass>()
                  .HasKey(t=>new { t.ApplicationUserId, t.GymClassId});

        // queryfilter here for showing gymclasses which starts in future only
            builder.Entity<GymClass>().HasQueryFilter(g => g.StartTime > DateTime.Now);

        }


    }
}
