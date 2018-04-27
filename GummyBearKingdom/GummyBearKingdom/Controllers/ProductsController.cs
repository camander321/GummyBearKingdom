
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
        private IPoductRepository ProductRepo;
        public ProductsController(IPoductRepository repo = null)
        {
            if (repo == null) ProductRepo = new EFProductRepository();
            else ProductRepo = repo;
        } 

        public IActionResult Index()
        {
            List<Product> model = ProductRepo.Products.ToList();
            return View(model);
        }

        public IActionResult Details(int id)
        {
            Product model = ProductRepo.Products.FirstOrDefault(product => product.ProductId == id);
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            ProductRepo.Save(product);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(ProductRepo.Products.FirstOrDefault(product => product.ProductId == id));
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            ProductRepo.Edit(product);
            return RedirectToAction("Details", new { id = product.ProductId });
        }

        public IActionResult Delete(int id)
        {
            Product model = ProductRepo.Products.FirstOrDefault(product => product.ProductId == id);
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Product product = ProductRepo.Products.FirstOrDefault(item => item.ProductId == id);
            ProductRepo.Remove(product);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAll()
        {
            return View();
        }

        [HttpPost, ActionName("DeleteAll")]
        public IActionResult DeleteAllConfirmed()
        {
            ProductRepo.RemoveAll();
            return RedirectToAction("Index");
        }
    }
}
