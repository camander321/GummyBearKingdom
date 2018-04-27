using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using GummyBearKingdom.Models;
using GummyBearKingdom.Tests;
using Moq;

namespace GummyBearKingdom.Controllers.Tests
{
    [TestClass]
    public class ReviewsControllerTests
    {
        private Mock<IReviewRepository> mock = new Mock<IReviewRepository>();
        private Mock<IProductRepository> mockProduct = new Mock<IProductRepository>();
        //EFProductRepository db = new EFProductRepository(new TestDbContext());
        //EFProductRepository dbProduct = new EFProductRepository(new TestDbContext());
        private void DbSetup()
        {
            mockProduct.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product { ProductId = 1, Name = "Test 1", Description = "Its a test", Cost = 5 },
                    new Product { ProductId = 2, Name = "Test 2", Description = "Its another test", Cost = 6 }
                }.AsQueryable());

            mock.Setup(m => m.Reviews).Returns(new Review[]
                {
                    new Review { Rating = 3, Content = "this is some content", ProductId = mockProduct.Object.Products.FirstOrDefault().ProductId },
                    new Review { Rating = 4, Content = "this is some more content", ProductId = mockProduct.Object.Products.LastOrDefault().ProductId }
                }.AsQueryable());
        }

        public void Dispose()
        {
            mock.Object.RemoveAll();
            mockProduct.Object.RemoveAll();
        }

        [TestMethod]
        public void Mock_GetViewResultIndex_ActionResult() // Confirms route returns view
        {
            DbSetup();
            ReviewsController controller = new ReviewsController(mock.Object);
            var result = controller.Index(mockProduct.Object.Products.FirstOrDefault().ProductId);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Mock_IndexModelContainsProducts_Collection()
        {
            DbSetup();
            ReviewsController controller = new ReviewsController(mock.Object);
            int productId = mockProduct.Object.Products.FirstOrDefault().ProductId;

            ViewResult indexView = controller.Index(productId) as ViewResult;
            var collection = indexView.ViewData.Model as List<Review>;

            Assert.IsInstanceOfType(collection, typeof(List<Review>));
        }
    }
}
