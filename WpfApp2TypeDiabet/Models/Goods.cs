using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2TypeDiabet.Models
{
    public class Goods
    {
        public int id { get; set; }
        public string GoodName { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public Goods(string goodName)
        {
            GoodName = goodName;
        }
    }
}
