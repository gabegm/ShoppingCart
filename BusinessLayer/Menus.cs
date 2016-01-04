using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class Menus : BLBase
    {
        public Menus() : base() { }
        public Menus(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        /// <summary>
        /// Returns all users and user accounts
        /// </summary>
        /// <returns>All users</returns>
        public IQueryable<CommonLayer.Menu> GetMenus()
        {
            return new DataLayer.DAMenus(this.Entities).GetMenus();
        }

        /// <summary>
        /// Get user with a specific email.
        /// </summary>
        /// <param name="Email">Email for which user will be returned.</param>
        /// <returns>One single user matching email.</returns>
        public CommonLayer.Menu GetMenu(string ID)
        {
            return new DataLayer.DAMenus(this.Entities).GetMenu(ID);
        }

        /// <summary>
        /// Adds a new to the database.
        /// </summary>
        /// <param name="User">user instance to be added.</param>
        public void AddMenuToDatabase(CommonLayer.Menu Menu)
        {
            Menu.ID = new Guid().ToString();
            new DataLayer.DAMenus(this.Entities).AddMenu(Menu);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="User">User changes to be updated to db.</param>
        public void UpdateMenu(CommonLayer.Menu Menu)
        {
            new DataLayer.DAMenus(this.Entities).UpdateMenu(Menu);
        }

        /// <summary>
        /// Deletes a user from database.
        /// </summary>
        /// <param name="User">User to delete.</param>
        public void DeleteMenu(string ID)
        {
            CommonLayer.Menu Menu = this.GetMenu(ID);

            if (Menu != null)
            {
                new DataLayer.DAMenus(this.Entities).DeleteMenu(Menu);
            }
        }
    }
}