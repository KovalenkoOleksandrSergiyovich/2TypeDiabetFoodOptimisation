using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2TypeDiabet.DBServices;
using WpfApp2TypeDiabet.Models;

namespace WpfApp2TypeDiabet.Services
{
    public class CategoryService
    {
        public Category GetCategory(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var caterory = from b in db.Categories
                               where b.Id == id
                               select b;
                return caterory.First();
            }
        }
        public Category GetCategory(string categoryName)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var caterory = from b in db.Categories
                               where b.CategoryName == categoryName
                               select b;
                return caterory.First();
            }
        }
        public List<string> FillCategories()
        {
            List<string> CategoryList = new List<string>();
            using (ApplicationContext db = new ApplicationContext())
            {
                var caterory = from b in db.Categories
                               select b;
                foreach(Category c in caterory)
                {
                    CategoryList.Add(c.CategoryName);
                }
            }
            return CategoryList;
        }
    }
}
