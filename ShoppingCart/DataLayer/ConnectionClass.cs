using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class ConnectionClass
    {
        public CommonLayer.DBModelEntities Entities { get; set; }

        public ConnectionClass()
        {
            this.Entities = new CommonLayer.DBModelEntities();
        }

        public ConnectionClass(CommonLayer.DBModelEntities Entity)
        {
            this.Entities = Entity;
        }
    }
}