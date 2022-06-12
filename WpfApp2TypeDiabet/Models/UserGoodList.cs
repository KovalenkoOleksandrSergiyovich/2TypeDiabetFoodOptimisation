using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2TypeDiabet.Models
{
    public class UserGoodList
    {
        public int id { get; set; }
        public int GoodInShopID { get; set; }
        public GoodInShop GoodInShop { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
