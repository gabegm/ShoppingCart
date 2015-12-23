﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class Carts : BLBase
    {
        public Carts() : base() { }

        public IQueryable<CommonLayer.Models.CartsModel> GetCartProductsAsModel()
        {
            return new DataLayer.DACarts(this.Entities).GetCartItemsAsModel();
        }
    }
}