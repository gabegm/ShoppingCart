using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class UiModels
    {

        public class LoginModel
        {

            public string Email { get; set; }
            public string Password { get; set; }

        }



        public class Product
        {
            private int _Id;

            public int Id
            {
                get { return _Id; }
                set { _Id = value; }
            }

            private string _Name;

            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }
            private int _Quantity;

            public int Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; }
            }
            private string _Description;

            public string Description
            {
                get { return _Description; }
                set { _Description = value; }
            }

        }




    }
}