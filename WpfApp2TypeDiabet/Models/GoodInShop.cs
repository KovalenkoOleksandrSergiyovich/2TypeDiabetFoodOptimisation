using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2TypeDiabet.Models
{
    public class GoodInShop
    {
        public int id { get; set; }
        public int GoodId { get; set; }
        public Goods Good { get; set; }
        public bool IsDefault { get; set; }
        public int RestrictionID { get; set; }
        public Restriction Restriction { get; set; }
    }
}
