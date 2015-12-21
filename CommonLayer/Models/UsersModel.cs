using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonLayer.Models
{
    public class UsersModel
    {
        public Guid ID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Contact { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string House { get; set; }
        public string Street { get; set; }
        public Guid TownID { get; set; }
        public string TownName { get; set; }
        public Guid CountryID { get; set; }
        public string CountryName { get; set; }
        public Guid RoleID { get; set; }
        public string RoleName { get; set; }
        public Guid UserAccountID { get; set; }
        public string UserAccountUsername { get; set; }
        public string UserAccountPassword { get; set; }
    }
}