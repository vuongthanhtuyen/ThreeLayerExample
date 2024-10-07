using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Manager
{
    public class CategoryDetailManagerBLL
    {

        public static CategoryDetail InsertCategory(CategoryDetail categoryDetail)
        {
            return new CategoryDetailController().Insert(categoryDetail);
        }
        

    }
}
