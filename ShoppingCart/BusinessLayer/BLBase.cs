using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class BLBase
    {
        public CommonLayer.DBModelEntities Entities { get; set; }

        public BLBase()
        {
            this.Entities = new CommonLayer.DBModelEntities();
        }

        public BLBase(CommonLayer.DBModelEntities Entities)
        {
            this.Entities = Entities;
        }
    }
}