using System;

namespace CommonLayer.Models
{
    public class AuditsModel
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public Guid UserID { get; set; }
        public string UserEmail { get; set; }
    }
}