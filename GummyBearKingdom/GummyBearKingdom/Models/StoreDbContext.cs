using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GummyBearKingdom;

namespace GummyBearKingdom.Models
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext() { }
        //public DbSet<Experience> Experiences { get; set; }
        //public DbSet<Location> Locations { get; set; }
        //public DbSet<Person> Persons { get; set; }
        //public DbSet<PersonExperience> PersonExperiences { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
            .UseMySql(Startup.ConnectionString);

        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}