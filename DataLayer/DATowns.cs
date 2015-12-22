using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class DATowns : ConnectionClass
    {
        public DATowns() : base() { }
        public DATowns(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.Town> GetTowns()
        {
            return this.Entities.Towns;
        }

        public IQueryable<CommonLayer.Models.TownsModel> GetTownsAsModel()
        {
            return (from town in this.Entities.Towns
                    join country in this.Entities.Countries on town.CountryID equals country.ID
                    select new CommonLayer.Models.TownsModel()
                    {
                        ID = town.ID,
                        Name = town.Name,
                        CountryID = town.CountryID,
                        CountryName = country.Name
                    });
        }

        public CommonLayer.Town GetTown(Guid town)
        {
            return this.Entities.Towns.SingleOrDefault(t => t.ID == town);
        }

        public void AddTown(CommonLayer.Town town)
        {
            this.Entities.Towns.Add(town);
            this.Entities.SaveChanges();
        }

        public void UpdateTown(CommonLayer.Town town)
        {
            CommonLayer.Town ExistingTown = this.GetTown(town.ID);
            this.Entities.Entry(ExistingTown).CurrentValues.SetValues(town);
            this.Entities.SaveChanges();
        }

        public void DeleteTown(CommonLayer.Town Town)
        {
            this.Entities.Towns.Remove(Town);
            this.Entities.SaveChanges();
        }

        public IQueryable<CommonLayer.Country> GetCountries()
        {
            return this.Entities.Countries;
        }
    }
}