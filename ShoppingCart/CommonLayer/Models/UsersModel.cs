using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonLayer.Models
{
    public class UsersModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public float Password { get; set; }
        public Guid RoleID { get; set; }
        public string RoleName { get; set; }
        public string CountryName { get; set; }
    }
}