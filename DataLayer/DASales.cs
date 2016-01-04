using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class DASales : ConnectionClass
    {
        public DASales() : base() { }
        public DASales(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.Sale> GetSales()
        {
            return this.Entities.Sales;
        }

        public CommonLayer.Sale GetSale(Guid ID)
        {
            return this.Entities.Sales.SingleOrDefault(ut => ut.ID.Equals(ID));
        }

        public void AddSale(CommonLayer.Sale Sale)
        {
            this.Entities.Sales.Add(Sale);
            this.Entities.SaveChanges();
        }

        public void UpdateSale(CommonLayer.Sale Sale)
        {
            CommonLayer.Sale ExistingSale = this.GetSale(Sale.ID);
            this.Entities.Entry(ExistingSale).CurrentValues.SetValues(Sale);
            this.Entities.SaveChanges();
        }

        public void DeleteSale(CommonLayer.Sale Sale)
        {
            this.Entities.Sales.Remove(Sale);
            this.Entities.SaveChanges();
        }
    }
}