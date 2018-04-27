using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using GummyBearKingdom.Controllers;
using GummyBearKingdom.Models;
using GummyBearKingdom.Tests;
using Moq;

namespace GummyBearKingdom.Controllers.Tests
{
    [TestClass]
    public class ProductsControllerTests : IDisposable
    {
        private Mock<IPoductRepository> mock = new Mock<IPoductRepository>();
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
        }

        [TestMethod]
        public void Mock_GetViewResultIndex_ActionResult() // Confirms route returns view
        {
            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);
            var result = controller.Index();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void Mock_IndexContainsModelData_List() // Confirms model as list of items
        {
            DbSetup();
            ViewResult indexView = new ProductsController(mock.Object).Index() as ViewResult;
            var result = indexView.ViewData.Model;
            Assert.IsInstanceOfType(result, typeof(List<Product>));
        }

        [TestMethod]
        public void Mock_IndexModelContainsAnimals_Collection()
        {
            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);
            Product product = new Product { ProductId = 2, Name = "Test 2", Description = "Its another test", Cost = 6 };

            ViewResult indexView = controller.Index() as ViewResult;
            List<Product> collection = indexView.ViewData.Model as List<Product>;

            CollectionAssert.Contains(collection, product);
        }

        [TestMethod]
        public void Mock_PostViewResultCreate_ViewResult()
        {
            Product product = new Product { ProductId = 3, Name = "Test 2", Description = "Its another test", Cost = 6 };

            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);
            var resultView = controller.Create(product);
            Assert.IsInstanceOfType(resultView, typeof(RedirectToActionResult));

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
