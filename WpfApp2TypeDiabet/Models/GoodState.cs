using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2TypeDiabet.Models
{
    public class GoodState
    {
        public int id  { get; set; }
        public string Name { get; set; }
        public ObservableCollection<GoodShopState> GoodShopState { get; set; } = new ObservableCollection<GoodShopState>();
    }
}
