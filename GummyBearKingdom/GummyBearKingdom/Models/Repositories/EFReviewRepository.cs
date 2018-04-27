using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GummyBearKingdom.Models
{
    public class EFReviewRepository : IReviewRepository
    {
        GummyBearDbContext db;
        public EFReviewRepository()
        {
            db = new GummyBearDbContext();
        }
        public EFReviewRepository(GummyBearDbContext thisDb)
        {
            db = thisDb;
        }

        public IQueryable<Review> Reviews { get { return db.Reviews; } }

        public Review Edit(Review review)
        {
            db.Entry(review).State = EntityState.Modified;
            db.SaveChanges();
            return review;
        }

        public void Remove(Review review)
        {
            db.Remove(review);
            db.SaveChanges();
        }

        public void RemoveAll()
        {
            db.Reviews.RemoveRange(db.Reviews);
            db.SaveChanges();
        }

        public Review Save(Review review)
        {
            db.Reviews.Add(review);
            db.SaveChanges();
            return review;
        }
    }
}
