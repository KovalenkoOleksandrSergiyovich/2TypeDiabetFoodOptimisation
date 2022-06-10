using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2TypeDiabet.Models
{
    public class GoodShopState
    {
        public int id { get; set; }
        public int GoodStateID { get; set; }
        public GoodState GoodState { get; set; }
        public int GoodInShopID { get; set; }
        public GoodInShop GoodInShop { get; set; }
    }
}
