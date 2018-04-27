using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GummyBearKingdom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GummyBearKingdom.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository ProductRepo;
        public HomeController(IProductRepository pRepo = null)
        {
            ProductRepo = pRepo ?? new EFProductRepository();
        }

        public IActionResult Index()
        {
            List<Product> model = ProductRepo.Products.Include(p => p.Reviews).OrderByDescending(p => p.GetAverageRating()).Take(3).ToList();
            return View(model);
        }
    }
}
