using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2TypeDiabet.Models
{
    public class GoodBasket
    {
        public int id { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public ObservableCollection<GoodInBasket> GoodsInBasket { get; set; } = new ObservableCollection<GoodInBasket>();
    }
}
