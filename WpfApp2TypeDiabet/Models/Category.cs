using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2TypeDiabet.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public ObservableCollection<Goods> Goods { get; set; }
        public Category(string categoryName)
        {
            CategoryName = categoryName;
        }
    }
}
