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

        [TestMethod]
        public void DB_SaveAddsNewProduct_Product()
        {
            Product product = new Product { Name = "Test 2", Description = "Its another test", Cost = 6 };
            List<Product> testList = new List<Product> { product };
            db.Save(product);

            CollectionAssert.AreEqual(testList, db.Products.ToList());
        }

        [TestMethod]
        public void DB_EditModifiesDbItem_Product()
        {
            string startName = "this is the product's old name";
            string newName = "this is the product's new name";
            Product product = new Product { Name = startName, Description = "Its another test", Cost = 6 };
            db.Save(product);

            product.Name = newName;
            db.Edit(product);

            Assert.AreEqual(newName, db.Products.FirstOrDefault(p => p.ProductId == product.ProductId).Name);
        }

        [TestMethod]
        public void DB_RemoveDeletesProduct_Void()
        {
            Product product = new Product { Name = "Test 2", Description = "Its another test", Cost = 6 };
            db.Save(product);
            CollectionAssert.Contains(db.Products.ToList(), product);

            db.Remove(product);

            CollectionAssert.DoesNotContain(db.Products.ToList(), product);
        }

        [TestMethod]
        public void DB_RemoveAllClearsTable_Void()
        {
            db.Save(new Product { Name = "Test 1", Description = "Its another test", Cost = 6 });
            db.Save(new Product { Name = "Test 2", Description = "Its one more test", Cost = 7 });
            Assert.AreEqual(2, db.Products.ToList().Count());

            db.RemoveAll();

            Assert.AreEqual(0, db.Products.ToList().Count());
        }
    }
}
