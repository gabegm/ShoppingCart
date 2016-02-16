using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class Search : BLBase
    {
        public Search() : base() { }
        public Search(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.Models.ProductsModel> Find(string Search)
        {
            return new DataLayer.DAProducts(this.Entities).Search(Search);
        }
    }
}