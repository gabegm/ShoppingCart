//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CommonLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Review
    {
        public System.Guid ID { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public System.DateTime Date { get; set; }
        public System.Guid UserID { get; set; }
        public System.Guid ProductID { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}