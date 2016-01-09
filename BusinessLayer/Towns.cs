using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class Towns : BLBase
    {
        public Towns() : base() { }
        public Towns(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.Town> GetTowns()
        {
            return new DataLayer.DATowns(this.Entities).GetTowns();
        }

        public IQueryable<CommonLayer.Models.TownsModel> GetTownsAsModel()
        {
            return new DataLayer.DATowns(this.Entities).GetTownsAsModel();
        }

        public void AddTownToDatabase(CommonLayer.Town Town)
        {
            Town.ID = Guid.NewGuid();
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
    }
}