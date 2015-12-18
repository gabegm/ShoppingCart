using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class Countries : BLBase
    {
        public Countries() : base() { }

        public IQueryable<CommonLayer.Country> GetCountries()
        {
            return new DataLayer.DACountries(this.Entities).GetCountries();
        }

        public void AddCountryToDatabase(CommonLayer.Country Country)
        {
            new DataLayer.DACountries(this.Entities).AddCountry(Country);

        }

        public void UpdateCountry(CommonLayer.Country country)
        {
            if (!string.IsNullOrEmpty(country.Name))
            {
                new DataLayer.DACountries(this.Entities).UpdateCountry(country);
            }
        }

        public CommonLayer.Country GetCountry(string name)
        {
            return new DataLayer.DACountries(this.Entities).GetCountry(name);
        }

        public void DeleteCountry(string name)
        {
            CommonLayer.Country country = this.GetCountry(name);
            if (country != null)
            {
                new DataLayer.DACountries(this.Entities).DeleteCountry(country);
            }
        }
    }
}