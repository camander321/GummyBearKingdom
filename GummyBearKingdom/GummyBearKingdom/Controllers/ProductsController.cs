
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GummyBearKingdom.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GummyBearKingdom.Controllers
{
    public class ProductsController : Controller
    {
        private StoreDbContext db = new StoreDbContext();

        public IActionResult Index()
        {
            List<Product> model = db.Products.ToList();
            return View(model);
        }

        public IActionResult Details(int id)
        {
            Product model = db.Products.FirstOrDefault(product => product.ProductId == id);
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Product model = db.Products.FirstOrDefault(product => product.ProductId == id);
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.FirstOrDefault(item => item.ProductId == id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAll()
        {
            return View();
        }

        [HttpPost, ActionName("DeleteAll")]
        public IActionResult DeleteAllConfirmed()
        {
            db.Products.RemoveRange(db.Products.ToList());
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
