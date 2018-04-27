using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GummyBearKingdom.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Cost { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public override bool Equals(System.Object otherProduct)
        {
            if (!(otherProduct is Product))  return false; 
            else  return this.ProductId.Equals(((Product)otherProduct).ProductId); 
        }

        public override int GetHashCode()
        {
            return this.ProductId.GetHashCode();
        }

        public double GetAverageRating()
        {
            return (Reviews.Sum(r => r.Rating) / (double)Reviews.Count());
        }
    }
}
