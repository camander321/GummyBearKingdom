using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GummyBearKingdom.Tests;

namespace GummyBearKingdom.Models.Tests
{
    [TestClass]
    public class ReviewTests : IDisposable
    {
        EFReviewRepository db = new EFReviewRepository(new TestDbContext());
        EFProductRepository dbProduct = new EFProductRepository(new TestDbContext());
        public void Dispose()
        {
            db.RemoveAll();
            dbProduct.RemoveAll();
        }

        private void DbSetup()
        {
            dbProduct.Save(new Product { Name = "Test 1", Description = "Its a test", Cost = 2 });
            dbProduct.Save(new Product { Name = "Test 2", Description = "Its another test", Cost = 6 });
        }

        [TestMethod]
        public void DB_DBStartsEmpty_Collection()
        {
            var reviews = db.Reviews.ToList();
            CollectionAssert.AreEqual(new List<Review>(), reviews);
        }

        [TestMethod]
        public void DB_SaveAddsNewReview_Review()
        {
            DbSetup();
            Review review = new Review { Rating = 3, Content = "this is some content", ProductId = dbProduct.Products.FirstOrDefault().ProductId };
            List<Review> testList = new List<Review> { review };
            db.Save(review);

            CollectionAssert.AreEqual(testList, db.Reviews.ToList());
        }

        [TestMethod]
        public void DB_EditModifiesDbItem_Product()
        {
            DbSetup();
            string startContent = "this is the reviews's old content";
            string newContent = "this is the reviews's new content";
            Review review = new Review { Rating = 3, Content = startContent, ProductId = dbProduct.Products.FirstOrDefault().ProductId };
            db.Save(review);

            review.Content = newContent;
            db.Edit(review);

            Assert.AreEqual(newContent, db.Reviews.FirstOrDefault(p => p.ProductId == review.ProductId).Content);
        }

        [TestMethod]
        public void DB_RemoveDeletesProduct_Void()
        {
            DbSetup();
            Review review = new Review { Rating = 3, Content = "this is some content", ProductId = dbProduct.Products.FirstOrDefault().ProductId };
            db.Save(review);
            CollectionAssert.Contains(db.Reviews.ToList(), review);

            db.Remove(review);

            CollectionAssert.DoesNotContain(db.Reviews.ToList(), review);
        }

        [TestMethod]
        public void DB_RemoveAllClearsTable_Void()
        {
            DbSetup();
            db.Save(new Review { Rating = 3, Content = "this is some content", ProductId = dbProduct.Products.FirstOrDefault().ProductId });
            db.Save(new Review { Rating = 4, Content = "this is some more content", ProductId = dbProduct.Products.LastOrDefault().ProductId });
            Assert.AreEqual(2, db.Reviews.ToList().Count());

            db.RemoveAll();

            Assert.AreEqual(0, db.Reviews.ToList().Count());
        }
    }
}
