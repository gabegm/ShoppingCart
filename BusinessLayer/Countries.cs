﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class Countries : BLBase
    {
        public Countries() : base() { }
        public Countries(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.Country> GetCountries()
        {
            return new DataLayer.DACountries(this.Entities).GetCountries();
        }

        public void AddCountryToDatabase(CommonLayer.Country Country)
        {
            Country.ID = Guid.NewGuid();
            new DataLayer.DACountries(this.Entities).AddCountry(Country);
        }

        public void UpdateCountry(CommonLayer.Country country)
        {
            if (!string.IsNullOrEmpty(country.Name))
            {
                new DataLayer.DACountries(this.Entities).UpdateCountry(country);
            }
        }

        public CommonLayer.Country GetCountry(Guid ID)
        {
            return new DataLayer.DACountries(this.Entities).GetCountry(ID);
        }

        public void DeleteCountry(Guid ID)
        {
            CommonLayer.Country country = this.GetCountry(ID);
            if (country != null)
            {
                new DataLayer.DACountries(this.Entities).DeleteCountry(country);
            }
        }
    }
}