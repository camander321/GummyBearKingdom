using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    }
}
