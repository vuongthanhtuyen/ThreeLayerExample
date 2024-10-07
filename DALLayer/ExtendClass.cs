using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DALLayer
{
    public class UserGetAllAndRoleName : User
    {
        public string RoleName { get; set; }
    }
    public class UserAndRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ma { get; set; }
        public int IsHaveRole { get; set; }
    }
    public class LoginReturnId
    {
        public int Id { get; set; }
    }

    public class MenuCheckByUser
    {
        public string MenuName { get; set; }
        public string MenuMa { get; set; }
        public string Url { get; set; }
        public string ParentMenu { get; set; }
        public string ParentMa { get; set; }
    }

    public class ParentMenuInfo
    {
        public string ParentMenu { get; set; }
        public string ParentMa { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is ParentMenuInfo other)
            {
                return ParentMenu == other.ParentMenu && ParentMa == other.ParentMa;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (ParentMenu, ParentMa).GetHashCode();
        }

    }

    public class MenuPermisstion
    {
        public string MenuMa { get; set; }
        public string PermissionMa { get; set; }
    }


    public class MenuPermissionDetail
    {
        public int MenuId { get; set; }
        public int PermissionId { get; set; }

        public string MenuName { get; set; }
        public string PermissionName { get; set; }
        public int IsRoleHavePermission { get; set; }
    }

    public class PostAndAuthorName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; } = null;
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int ViewCount { get; set; }
        public DateTime DatetimeCreate { get; set; }
        public bool Active { get; set; }
        public int AuthorID { get; set; }
        //public int CategoryId { get; set; }
        public string AuthorFullName { get; set; }
    }

    public class CategoryPostId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ma { get; set; }
        public int IsHaveCategory { get; set; }
    }


    public class PagingExtend
    {

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRow { get; set; }
        public string keySearch { get; set; }
    }

    public class CategoryAndPostExtend
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string PostName { get; set; }
    }
}
