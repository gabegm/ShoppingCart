using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class DAAudits : ConnectionClass
    {
        public DAAudits() : base() { }
        public DAAudits(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.Models.AuditsModel> GetAuditsAsModel()
        {
            return (from Audit in this.Entities.Audits
                    join User in this.Entities.Users on Audit.UserID equals User.ID into au
                    from SubUser in au.DefaultIfEmpty()
                    select new CommonLayer.Models.AuditsModel()
                    {
                        ID = Audit.ID,
                        Description = Audit.Description,
                        Type = Audit.Description,
                        UserID = (SubUser == null ? Guid.Empty : SubUser.ID),
                        UserEmail = (SubUser == null ? String.Empty : SubUser.Email)
                    });
        }

        public IQueryable<CommonLayer.Audit> GetAudits()
        {
            return this.Entities.Audits;
        }

        public CommonLayer.Audit GetAudit(Guid ID)
        {
            return this.Entities.Audits.SingleOrDefault(ut => ut.ID.Equals(ID));
        }

        public void AddAudit(CommonLayer.Audit Audit)
        {
            this.Entities.Audits.Add(Audit);
            this.Entities.SaveChanges();
        }

        public void UpdateAudit(CommonLayer.Audit Audit)
        {
            CommonLayer.Audit ExistingAudit = this.GetAudit(Audit.ID);
            this.Entities.Entry(ExistingAudit).CurrentValues.SetValues(Audit);
            this.Entities.SaveChanges();
        }

        public void DeleteAudit(CommonLayer.Audit Audit)
        {
            this.Entities.Audits.Remove(Audit);
            this.Entities.SaveChanges();
        }
    }
}