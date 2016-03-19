using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class DAReviews : ConnectionClass
    {
        public DAReviews() : base() { }
        public DAReviews(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.Review> GetReviews()
        {
            return this.Entities.Reviews;
        }

        public CommonLayer.Review GetReview(Guid ID)
        {
            return this.Entities.Reviews.SingleOrDefault(ut => ut.ID.Equals(ID));
        }

        public IQueryable<CommonLayer.Review> GetReviews(Guid ProductID)
        {
            return (from review in this.Entities.Reviews
                    where review.ProductID == ProductID
                    select review
                    );
        }

        public void AddReview(CommonLayer.Review Review)
        {
            this.Entities.Reviews.Add(Review);
            this.Entities.SaveChanges();
        }

        public void UpdateReview(CommonLayer.Review Review)
        {
            CommonLayer.Review ExistingReview = this.GetReview(Review.ID);
            this.Entities.Entry(ExistingReview).CurrentValues.SetValues(Review);
            this.Entities.SaveChanges();
        }

        public void DeleteReview(CommonLayer.Review Review)
        {
            this.Entities.Reviews.Remove(Review);
            this.Entities.SaveChanges();
        }
    }
}