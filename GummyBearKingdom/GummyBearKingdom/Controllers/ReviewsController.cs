using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GummyBearKingdom.Models;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GummyBearKingdom.Controllers
{
    public class ReviewsController : Controller
    {
        private IReviewRepository ReviewRepo;
        public ReviewsController(IReviewRepository repo = null)
        {
            ReviewRepo = repo ?? new EFReviewRepository();        }

        public IActionResult Index(int productId)
        {
            return View(ReviewRepo.Reviews.Where(r => r.ProductId == productId).ToList());
        }
    }
}
