using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GummyBearKingdom.Models;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GummyBearKingdom.Controllers
{
    public class ReviewsController : Controller
    {
        private IProductRepository ProductRepo;
        private IReviewRepository ReviewRepo;
        public ReviewsController(IProductRepository pRepo = null, IReviewRepository rRepo = null)
        {
            ProductRepo = pRepo ?? new EFProductRepository();
            ReviewRepo = rRepo ?? new EFReviewRepository();
        }

        public IActionResult Index(int id)
        {
            List<Review> model = ReviewRepo.Reviews.Where(r => r.ProductId == id).ToList();
            Product product = ProductRepo.Products.FirstOrDefault(p => p.ProductId == id);
            product.Reviews = model;
            model.ForEach(r => r.ProductId = id);
            ViewBag.Product = product;
            return View(model);
        }

        public IActionResult Create(int id)
        {
            ViewBag.Product = ProductRepo.Products.FirstOrDefault(p => p.ProductId == id);
            return View(new Review() { ProductId = id });
        }

        [HttpPost]
        public IActionResult Create(Review review)
        {
            if (review.RatingInRange() && review.ContentShortEnough())
            {
                ReviewRepo.Save(review);
                return RedirectToAction("Index", new { id = review.ProductId });
            }
            return RedirectToAction("Create", new { id = review.ProductId });
        }
    }
}
