using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class CarouselItems : BLBase
    {
        public CarouselItems() : base() { }
        public CarouselItems(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        /// <summary>
        /// Returns all users and user accounts
        /// </summary>
        /// <returns>All users</returns>
        public IQueryable<CommonLayer.CarouselItem> GetCarouselItems()
        {
            return new DataLayer.DACarouselItems(this.Entities).GetCarouselItems();
        }

        /// <summary>
        /// Get user with a specific email.
        /// </summary>
        /// <param name="Email">Email for which user will be returned.</param>
        /// <returns>One single user matching email.</returns>
        public CommonLayer.CarouselItem GetCarouselItem(Guid ID)
        {
            return new DataLayer.DACarouselItems(this.Entities).GetCarouselItem(ID);
        }

        /// <summary>
        /// Adds a new to the database.
        /// </summary>
        /// <param name="User">user instance to be added.</param>
        public void AddCarouselItemToDatabase(CommonLayer.CarouselItem CarouselItem)
        {
            CarouselItem.ID = Guid.NewGuid();
            new DataLayer.DACarouselItems(this.Entities).AddCarouselItem(CarouselItem);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="User">User changes to be updated to db.</param>
        public void UpdateCarouselItem(CommonLayer.CarouselItem CarouselItem)
        {
            new DataLayer.DACarouselItems(this.Entities).UpdateCarouselItem(CarouselItem);
        }

        /// <summary>
        /// Deletes a user from database.
        /// </summary>
        /// <param name="User">User to delete.</param>
        public void DeleteCarouselItem(Guid ID)
        {
            CommonLayer.CarouselItem CarouselItem = this.GetCarouselItem(ID);

            if (CarouselItem != null)
            {
                new DataLayer.DACarouselItems(this.Entities).DeleteCarouselItem(CarouselItem);
            }
        }
    }
}