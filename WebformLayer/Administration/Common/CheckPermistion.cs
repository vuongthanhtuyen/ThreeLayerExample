using DALLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebformLayer.Administration.LeftNavbar
{
    public class CheckPermistion
    {
        public const string Them = "Them";
        public const string Sua = "Sua";
        public const string Xoa = "Xoa";
        public const string Xem = "Xem";
        public List<MenuPermisstion> GetMenuPermisstions(object permission, string menuParent)
        {
            List<MenuPermisstion> menuPermisstions = permission as List<MenuPermisstion>;
            List<MenuPermisstion> menuPermissionChilds = new List<MenuPermisstion>(); // Initialize the list

            if (menuPermisstions != null && menuPermisstions.Count > 0)
            {
                foreach (var menu in menuPermisstions)
                {
                    if (menu.MenuMa == menuParent)
                    {
                        menuPermissionChilds.Add(new MenuPermisstion
                        {
                            MenuMa = menuParent,
                            PermissionMa = menu.PermissionMa
                        });
                    }
                }
            }
            return menuPermissionChilds;
        }


        public bool CheckPermission(List<MenuPermisstion> permissionChild,string menuParent,string Quyen)
        {
            foreach (var menu in permissionChild)
            {
                if (menu.MenuMa == menuParent && menu.PermissionMa == Quyen)
                {
                    return true;
                }
            }
            return false;
        }
        
    }
}