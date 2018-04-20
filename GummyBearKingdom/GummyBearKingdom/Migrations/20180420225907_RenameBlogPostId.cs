using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GummyBearKingdom.Migrations
{
    public partial class RenameBlogPostId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BlogPostKey",
                table: "BlogPosts",
                newName: "BlogPostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BlogPostId",
                table: "BlogPosts",
                newName: "BlogPostKey");
        }
    }
}
