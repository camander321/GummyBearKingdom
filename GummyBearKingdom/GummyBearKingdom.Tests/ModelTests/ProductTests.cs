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
            //db.RemoveAll();
        }

        [TestMethod]
        public void DB_DBStartsEmpty_Product()
        {
            var products = db.Products.ToList();
            CollectionAssert.AreEqual(new List<Product>(), products);
        }

        [TestMethod]
        public void DB_SaveAddsNewProduct_Product()
        {
            Product product = new Product { Name = "Test 2", Description = "Its another test", Cost = 6 };
            List<Product> testList = new List<Product>();

            testList.Add(product);
            db.Save(product);

            CollectionAssert.AreEqual(testList, db.Products.ToList());
        }
    }
}
