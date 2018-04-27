using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GummyBearKingdom.Models;

namespace GummyBearKingdom.Tests
{
    class TestDbContext : GummyBearDbContext
    {

        public override DbSet<Product> Products { get; set; }
        public override DbSet<Review> Reviews { get; set; }
        public override DbSet<BlogPost> BlogPosts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
            .UseMySql("Server=localhost;Port=8889;database=gummyBearStore_Test;uid=root;pwd=root;");
    }
}
