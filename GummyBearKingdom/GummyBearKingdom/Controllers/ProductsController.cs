using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GummyBearKingdom.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GummyBearKingdom.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository ProductRepo;
        private IReviewRepository ReviewRepo;
        public ProductsController(IProductRepository pRepo = null, IReviewRepository rRepo = null)
        {
            ProductRepo = pRepo ?? new EFProductRepository();
            ReviewRepo = rRepo ?? new EFReviewRepository();
        }

        public IActionResult Index()
        {
            List<Product> model = ProductRepo.Products.ToList();
            return View(model);
        }

        public IActionResult Details(int id)
        {
            Product model = ProductRepo.Products.FirstOrDefault(product => product.ProductId == id);
            model.Reviews = ReviewRepo.Reviews.Where(r => r.ProductId == id).ToList();
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
