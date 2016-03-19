using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonLayer.Models
{
    public class MenusModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public bool Active { get; set; }
        public string ParentID { get; set; }
        public string ParentName { get; set; }
    }
}