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

        public CommonLayer.Country GetCountry(string country)
        {
            return this.Entities.Countries.SingleOrDefault(c => c.Name == country);
        }

        public void AddCountry(CommonLayer.Country country)
        {
            this.Entities.Countries.Add(country);
            this.Entities.SaveChanges();
        }

        public void UpdateCountry(CommonLayer.Country country)
        {
            CommonLayer.Country ExistingCountry = this.GetCountry(country.Name);
            this.Entities.Entry(ExistingCountry).CurrentValues.SetValues(country);
            this.Entities.SaveChanges();
        }
    }
}