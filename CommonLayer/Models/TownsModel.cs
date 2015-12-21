using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonLayer.Models
{
    public class TownsModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid CountryID { get; set; }
        public string CountryName { get; set; }
    }
}