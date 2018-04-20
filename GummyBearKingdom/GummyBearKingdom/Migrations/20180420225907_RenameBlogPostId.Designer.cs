using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GummyBearKingdom.Models;

namespace GummyBearKingdom.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    [Migration("20180420225907_RenameBlogPostId")]
    partial class RenameBlogPostId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("GummyBearKingdom.Models.BlogPost", b =>
                {
                    b.Property<int>("BlogPostId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<string>("Content");

                    b.Property<DateTime>("PostTime");

                    b.Property<string>("Title");

                    b.HasKey("BlogPostId");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("GummyBearKingdom.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Cost");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });
        }
    }
}
