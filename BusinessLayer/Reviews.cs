using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class Reviews : BLBase
    {
        public Reviews() : base() { }
        public Reviews(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        /// <summary>
        /// Returns all users and user accounts
        /// </summary>
        /// <returns>All users</returns>
        public IQueryable<CommonLayer.Review> GetReviews()
        {
            return new DataLayer.DAReviews(this.Entities).GetReviews();
        }

        /// <summary>
        /// Get user with a specific email.
        /// </summary>
        /// <param name="Email">Email for which user will be returned.</param>
        /// <returns>One single user matching email.</returns>
        public CommonLayer.Review GetReview(Guid ID)
        {
            return new DataLayer.DAReviews(this.Entities).GetReview(ID);
        }

        /// <summary>
        /// Adds a new to the database.
        /// </summary>
        /// <param name="User">user instance to be added.</param>
        public void AddReviewToDatabase(CommonLayer.Review Review)
        {
            Review.ID = Guid.NewGuid();
            new DataLayer.DAReviews(this.Entities).AddReview(Review);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="User">User changes to be updated to db.</param>
        public void UpdateReview(CommonLayer.Review Review)
        {
            new DataLayer.DAReviews(this.Entities).UpdateReview(Review);
        }

        /// <summary>
        /// Deletes a user from database.
        /// </summary>
        /// <param name="User">User to delete.</param>
        public void DeleteReview(Guid ID)
        {
            CommonLayer.Review Review = this.GetReview(ID);

            if (Review != null)
            {
                new DataLayer.DAReviews(this.Entities).DeleteReview(Review);
            }
        }
    }
}