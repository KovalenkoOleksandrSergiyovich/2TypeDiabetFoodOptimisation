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
        public double Amount { get; set; }
        public int GoodInShopID { get; set; }
        public GoodInBasket(int goodInShopID, double amount)
        {
            GoodInShopID = goodInShopID;
            Amount = amount;
        }
    }
}
