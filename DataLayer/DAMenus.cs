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

        public IQueryable<CommonLayer.Models.MenusModel> GetMenusAsModel()
        {
            return (from Menu in this.Entities.Menus
                    join ParentMenu in this.Entities.Menus on Menu.ParentID equals ParentMenu.ID into mp
                    from SubMenu in mp.DefaultIfEmpty()
                    select new CommonLayer.Models.MenusModel()
                    {
                        ID = Menu.ID,
                        Name = Menu.Name,
                        ParentID = Menu.ParentID,
                        ParentName = (SubMenu == null ? String.Empty : SubMenu.Name)
                    });
        }

        public IQueryable<CommonLayer.Models.MenusModel> GetParentMenusAsModel()
        {
            return (from Menu in this.Entities.Menus
                    join ParentMenu in this.Entities.Menus on Menu.ParentID equals ParentMenu.ID into mp
                    where Menu.ParentID == null
                    from SubMenu in mp.DefaultIfEmpty()
                    select new CommonLayer.Models.MenusModel()
                    {
                        ID = Menu.ID,
                        Name = Menu.Name,
                        ParentID = Menu.ParentID,
                        ParentName = (SubMenu == null ? String.Empty : SubMenu.Name)
                    });
        }

        public IQueryable<CommonLayer.Models.MenusModel> GetSubMenusAsModel()
        {
            return (from Menu in this.Entities.Menus
                    join ParentMenu in this.Entities.Menus on Menu.ParentID equals ParentMenu.ID into mp
                    where Menu.ParentID != null
                    from SubMenu in mp.DefaultIfEmpty()
                    select new CommonLayer.Models.MenusModel()
                    {
                        ID = Menu.ID,
                        Name = Menu.Name,
                        ParentID = Menu.ParentID,
                        ParentName = (SubMenu == null ? String.Empty : SubMenu.Name)
                    });
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