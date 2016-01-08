using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class Audits : BLBase
    {
        public Audits() : base() { }
        public Audits(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        /// <summary>
        /// Returns all users and user accounts
        /// </summary>
        /// <returns>All users</returns>
        public IQueryable<CommonLayer.Audit> GetAudits()
        {
            return new DataLayer.DAAudits(this.Entities).GetAudits();
        }

        public IQueryable<CommonLayer.Models.AuditsModel> GetAuditsAsModel()
        {
            return new DataLayer.DAAudits(this.Entities).GetAuditsAsModel();
        }

        /// <summary>
        /// Get user with a specific email.
        /// </summary>
        /// <param name="Email">Email for which user will be returned.</param>
        /// <returns>One single user matching email.</returns>
        public CommonLayer.Audit GetAudit(Guid ID)
        {
            return new DataLayer.DAAudits(this.Entities).GetAudit(ID);
        }

        /// <summary>
        /// Adds a new to the database.
        /// </summary>
        /// <param name="User">user instance to be added.</param>
        public void AddAudit(Guid UserID, string Description, string Type)
        {
            CommonLayer.Audit Audit = new CommonLayer.Audit();

            Audit.ID = Guid.NewGuid();
            Audit.Description = Description;
            Audit.Type = Type;

            if (UserID.Equals(Guid.Empty))
            {
                Audit.UserID = null;
            }
            else
            {
                Audit.UserID = UserID;
            }

            new DataLayer.DAAudits(this.Entities).AddAudit(Audit);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="User">User changes to be updated to db.</param>
        public void UpdateAudit(CommonLayer.Audit Audit)
        {
            new DataLayer.DAAudits(this.Entities).UpdateAudit(Audit);
        }

        /// <summary>
        /// Deletes a user from database.
        /// </summary>
        /// <param name="User">User to delete.</param>
        public void DeleteAudit(Guid ID)
        {
            CommonLayer.Audit Audit = this.GetAudit(ID);

            if (Audit != null)
            {
                new DataLayer.DAAudits(this.Entities).DeleteAudit(Audit);
            }
        }
    }
}