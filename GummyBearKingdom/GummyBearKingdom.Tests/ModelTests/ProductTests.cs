using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using GummyBearKingdom.Controllers;
using GummyBearKingdom.Models;
using GummyBearKingdom.Tests;
using Moq;

namespace GummyBearKingdom.Models.Tests
{
    [TestClass]
    public class ProductTests : IDisposable
    {
        EFProductRepository db = new EFProductRepository(new TestDbContext());
        public void Dispose()
        {
            db.RemoveAll();
        }

        [TestMethod]
        public void DB_DBStartsEmpty_Product()
        {
            var products = db.Products.ToList();
            CollectionAssert.AreEqual(new List<Product>(), products);
        }

        //[TestMethod]
        //public void DB_SaveAddsNewProduct_Product()
        //{

        //}
    }
}
