﻿using System;
using System.Collections.Generic;

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
        public bool Active { get; set; }
        public string House { get; set; }
        public string Street { get; set; }
        public Guid TownID { get; set; }
        public string TownName { get; set; }
        public Guid CountryID { get; set; }
        public string CountryName { get; set; }
        public Guid RoleID { get; set; }
        //public List<Role> Role { get; set; }
        public string RoleName { get; set; }
        public Guid UserTypeID { get; set; }
        public string UserTypeName { get; set; }
        public Guid UserAccountID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}