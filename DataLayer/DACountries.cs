using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class DACountries : ConnectionClass
    {
        public DACountries() : base() { }
        public DACountries(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.Country> GetCountries()
        {
            return this.Entities.Countries;
        }

        public CommonLayer.Country GetCountry(Guid ID)
        {
            return this.Entities.Countries.SingleOrDefault(c => c.ID == ID);
        }

        public void AddCountry(CommonLayer.Country country)
        {
            this.Entities.Countries.Add(country);
            this.Entities.SaveChanges();
        }

        public void UpdateCountry(CommonLayer.Country country)
        {
            CommonLayer.Country ExistingCountry = this.GetCountry(country.ID);
            this.Entities.Entry(ExistingCountry).CurrentValues.SetValues(country);
            this.Entities.SaveChanges();
        }

        public void DeleteCountry(CommonLayer.Country Country)
        {
            this.Entities.Countries.Remove(Country);
            this.Entities.SaveChanges();
        }
    }
}