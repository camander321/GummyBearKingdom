using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GummyBearKingdom.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GummyBearKingdom.Controllers
{
    public class BlogsController : Controller
    {
        private GummyBearDbContext db = new GummyBearDbContext();
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(db.BlogPosts.OrderByDescending(model => model.PostTime).Take(10).ToList());
        }

        public IActionResult Details(int id)
        {
            return View(db.BlogPosts.FirstOrDefault(model => model.BlogPostId == id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BlogPost post)
        {
            post.PostTime = DateTime.Now;
            db.Add(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
