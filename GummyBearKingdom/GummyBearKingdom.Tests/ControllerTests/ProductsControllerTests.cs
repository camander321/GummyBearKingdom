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
    public class ProductsControllerTests : IDisposable
    {
        private Mock<IProductRepository> mock = new Mock<IProductRepository>();
        EFProductRepository db = new EFProductRepository(new TestDbContext());
        private void DbSetup()
        {
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product { ProductId = 1, Name = "Test 1", Description = "Its a test", Cost = 5 },
                    new Product { ProductId = 2, Name = "Test 2", Description = "Its another test", Cost = 6 }
                }.AsQueryable());
        }

        public void Dispose()
        {
            mock.Object.RemoveAll();
            db.RemoveAll();
        }

        [TestMethod]
        public void Mock_GetViewResultIndex_ActionResult() // Confirms route returns view
        {
            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);
            var result = controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Mock_GetViewResultDetails_ActionResult() // Confirms route returns view
        {
            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);
            var result = controller.Details(2);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Mock_GetViewResultCreateGet_ActionResult() // Confirms route returns view
        {
            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);
            var result = controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Mock_GetViewResultCreatePost_ActionResult() // Confirms route returns view
        {
            DbSetup();
            Product product = new Product { ProductId = 3, Name = "Test 3", Description = "Its one more test", Cost = 6 };
            ProductsController controller = new ProductsController(mock.Object);
            var result = controller.Create(product);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }
        [TestMethod]
        public void Mock_GetViewResultEditGet_ActionResult() // Confirms route returns view
        {
            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);
            var result = controller.Edit(2);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Mock_GetViewResultEditPost_ActionResult() // Confirms route returns view
        {
            DbSetup();
            Product product = new Product { ProductId = 3, Name = "Test 3", Description = "Its one more test", Cost = 6 };
            ProductsController controller = new ProductsController(mock.Object);
            var result = controller.Edit(product);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }
        [TestMethod]
        public void Mock_GetViewResultDeleteGet_ActionResult() // Confirms route returns view
        {
            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);
            var result = controller.Delete(2);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Mock_GetViewResultDeletePost_ActionResult() // Confirms route returns view
        {
            DbSetup();
            Product product = new Product { ProductId = 3, Name = "Test 3", Description = "Its one more test", Cost = 6 };
            ProductsController controller = new ProductsController(mock.Object);
            var result = controller.DeleteConfirmed(2);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }
        [TestMethod]
        public void Mock_GetViewResultDeleteAllGet_ActionResult() // Confirms route returns view
        {
            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);
            var result = controller.DeleteAll();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Mock_GetViewResultDeleteAllPost_ActionResult() // Confirms route returns view
        {
            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);
            var result = controller.DeleteAllConfirmed();
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void Mock_IndexModelContainsProducts_Collection()
        {
            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);
            Product product = new Product { ProductId = 2, Name = "Test 2", Description = "Its another test", Cost = 6 };

            ViewResult indexView = controller.Index() as ViewResult;
            List<Product> collection = indexView.ViewData.Model as List<Product>;

            CollectionAssert.Contains(collection, product);
        }

        [TestMethod]
        public void Mock_DetailsModelContainsProduct_Product()
        {
            DbSetup();
            Product product = mock.Object.Products.FirstOrDefault();
            ProductsController controller = new ProductsController(mock.Object);
            var resultView = controller.Details(product.ProductId) as ViewResult;
            var model = resultView.ViewData.Model as Product;
            Assert.IsInstanceOfType(model, typeof(Product));
            Assert.AreEqual(product, model);
        }

        [TestMethod]
        public void Mock_EditGetModelContainsProduct_Product()
        {
            DbSetup();
            Product product = mock.Object.Products.FirstOrDefault();
            ProductsController controller = new ProductsController(mock.Object);
            var resultView = controller.Edit(product.ProductId) as ViewResult;
            var model = resultView.ViewData.Model as Product;
            Assert.IsInstanceOfType(model, typeof(Product));
            Assert.AreEqual(product, model);
        }

        [TestMethod]
        public void Mock_DeleteGetModelContainsProduct_Product()
        {
            DbSetup();
            Product product = mock.Object.Products.FirstOrDefault();
            ProductsController controller = new ProductsController(mock.Object);
            var resultView = controller.Delete(product.ProductId) as ViewResult;
            var model = resultView.ViewData.Model as Product;
            Assert.IsInstanceOfType(model, typeof(Product));
            Assert.AreEqual(product, model);
        }


        [TestMethod]
        public void DB_CreatesNewEntries_Collection()
        {
            ProductsController controller = new ProductsController(db);
            Product product = new Product { Name = "Test 2", Description = "Its another test", Cost = 6 };
            var resultView = controller.Create(product);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Product>;
            CollectionAssert.Contains(collection, product);
        }
    }
}
