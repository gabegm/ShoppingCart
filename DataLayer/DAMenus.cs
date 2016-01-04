using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class DAMenus : ConnectionClass
    {
        public DAMenus() : base() { }
        public DAMenus(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.Menu> GetMenus()
        {
            return this.Entities.Menus;
        }

        public CommonLayer.Menu GetMenu(string ID)
        {
            return this.Entities.Menus.SingleOrDefault(ut => ut.ID.Equals(ID));
        }

        public void AddMenu(CommonLayer.Menu Menu)
        {
            this.Entities.Menus.Add(Menu);
            this.Entities.SaveChanges();
        }

        public void UpdateMenu(CommonLayer.Menu Menu)
        {
            CommonLayer.Menu ExistingMenu = this.GetMenu(Menu.ID);
            this.Entities.Entry(ExistingMenu).CurrentValues.SetValues(Menu);
            this.Entities.SaveChanges();
        }

        public void DeleteMenu(CommonLayer.Menu Menu)
        {
            this.Entities.Menus.Remove(Menu);
            this.Entities.SaveChanges();
        }
    }
}