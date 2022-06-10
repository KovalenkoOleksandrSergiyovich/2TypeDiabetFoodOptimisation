using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2TypeDiabet.Models
{
    public class GoodInBasket
    {
        public int id { get; set; }
        public int GoodBasketID { get; set; }
        public GoodBasket GoodBasket { get; set; }
        public int GoodShopStateID { get; set; }
        public GoodShopState GoodShopState { get; set; }
    }
}
