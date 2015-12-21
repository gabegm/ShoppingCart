using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class Towns : BLBase
    {
        public Towns() : base() { }

        public IQueryable<CommonLayer.Town> GetTowns()
        {
            return new DataLayer.DATowns(this.Entities).GetTowns();
        }

        public void AddTownToDatabase(CommonLayer.Town Town)
        {
            new DataLayer.DATowns(this.Entities).AddTown(Town);

        }

        public void UpdateTown(CommonLayer.Town town)
        {
            if (!string.IsNullOrEmpty(town.Name))
            {
                new DataLayer.DATowns(this.Entities).UpdateTown(town);
            }
        }

        public CommonLayer.Town GetTown(Guid ID)
        {
            return new DataLayer.DATowns(this.Entities).GetTown(ID);
        }

        public void DeleteTown(Guid ID)
        {
            CommonLayer.Town town = this.GetTown(ID);
            if (town != null)
            {
                new DataLayer.DATowns(this.Entities).DeleteTown(town);
            }
        }

        public IQueryable<CommonLayer.Country> GetCountries()
        {
            return new DataLayer.DATowns(this.Entities).GetCountries();
        }
    }
}