using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class DACarouselItems : ConnectionClass
    {
        public DACarouselItems() : base() { }
        public DACarouselItems(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.CarouselItem> GetCarouselItems()
        {
            return this.Entities.CarouselItems;
        }

        public CommonLayer.CarouselItem GetCarouselItem(Guid ID)
        {
            return this.Entities.CarouselItems.SingleOrDefault(ut => ut.ID.Equals(ID));
        }

        public void AddCarouselItem(CommonLayer.CarouselItem CarouselItem)
        {
            this.Entities.CarouselItems.Add(CarouselItem);
            this.Entities.SaveChanges();
        }

        public void UpdateCarouselItem(CommonLayer.CarouselItem CarouselItem)
        {
            CommonLayer.CarouselItem ExistingCarouselItem = this.GetCarouselItem(CarouselItem.ID);
            this.Entities.Entry(ExistingCarouselItem).CurrentValues.SetValues(CarouselItem);
            this.Entities.SaveChanges();
        }

        public void DeleteCarouselItem(CommonLayer.CarouselItem CarouselItem)
        {
            this.Entities.CarouselItems.Remove(CarouselItem);
            this.Entities.SaveChanges();
        }
    }
}