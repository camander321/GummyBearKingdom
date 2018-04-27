using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GummyBearKingdom.Models
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }

        [ForeignKey("Products")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public override bool Equals(System.Object otherReview)
        {
            if (!(otherReview is Review)) return false;
            else return ProductId.Equals(((Review)otherReview).ProductId);
        }

        public override int GetHashCode()
        {
            return this.ReviewId.GetHashCode();
        }

        public bool RatingInRange()
        {
            return Rating >= 1 && Rating <= 5;
        }

        public bool ContentShortEnough()
        {
            return Content.Length <= 255;
        }
    }
}
