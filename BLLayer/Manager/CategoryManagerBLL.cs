using DALLayer;
using SubSonic;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Manager
{
    public class CategoryManagerBLL
    {
        public static Category Insert(Category category)
        {
            return new CategoryController().Insert(category);
        }

        public static Category Update(Category category)
        {
            return new CategoryController().Update(category);
        }
        public static bool Delete(int categoryId) { 

            new Delete().From(CategoryDetail.Schema)
                .Where(CategoryDetail.CategoryIdColumn)
                .IsEqualTo(categoryId).Execute();

            return new CategoryController().Delete(categoryId);
        }

        public static CategoryCollection GetAll()
        {
            return new CategoryController().FetchAll();
        }
        public static Category GetById(int categoryId)
        {
            return new CategoryController().FetchByID(categoryId).SingleOrDefault();
        }


        public static Category IsExistsCategoryMa(string categoryMa)
        {
            Select select = new Select();
            select.From(Category.Schema).Where(Category.MaColumn).IsEqualTo(categoryMa);
            return select.ExecuteSingle<Category>();

        }
        public static Category IsExistsCategoryMaOtherID(string categoryMa, int categoryId )
        {
            Select select = new Select();
            select.From(Category.Schema).Where(Category.MaColumn).IsEqualTo(categoryMa)
            .And(Category.IdColumn).IsNotEqualTo(categoryId);
            return select.ExecuteSingle<Category>();

        }

        public static List<Category> GetAllSeachAndPaging(int? PageSize, int? PageIndex, string Key, bool? ASC, out int rowsCount)
        {
            rowsCount = 0;
            StoredProcedure sp = SPs.SearchPagingCategory(PageSize, PageIndex, Key, ASC, out rowsCount);
            //if (sp.OutputValues.Count > 0)
            //    rowsCount = Convert.ToInt32(sp.OutputValues[0]);

            DataSet ds = sp.GetDataSet();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (sp.OutputValues.Count > 0)
                    rowsCount = Convert.ToInt32(sp.OutputValues[0]);

            }

            return sp.ExecuteTypedList<Category>();
        }
    }
}
