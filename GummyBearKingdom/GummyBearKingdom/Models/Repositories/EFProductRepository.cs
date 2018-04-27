using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GummyBearKingdom.Models
{
    public class EFProductRepository : IPoductRepository
    {
        GummyBearDbContext db;
        public EFProductRepository()
        {
            db = new GummyBearDbContext();
        }
        public EFProductRepository(GummyBearDbContext thisDb)
        {
            db = thisDb;
        }


        public IQueryable<Product> Products  { get { return db.Products; } }

        public Product Edit(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return product;
        }

        public void Remove(Product product)
        {
            db.Products.Remove(product);
            db.SaveChanges();
        }

        public Product Save(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return product;
        }

        public void RemoveAll()
        {
            db.Products.RemoveRange(db.Products);
            db.SaveChanges();
        }
    }
}
