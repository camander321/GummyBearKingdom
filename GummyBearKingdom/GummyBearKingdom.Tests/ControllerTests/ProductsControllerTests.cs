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

    }
}
