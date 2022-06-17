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
        public double BU { get; set; }
        public double Price { get; set; }
        //public int GoodInBasketID { get; set; }
        public public ObservableCollection<GoodInBasket> GoodInBasket { get; set; } = new ObservableCollection<GoodInBasket>();

        public GoodBasket(int userID, double bU, double price, ObservableCollection<GoodInBasket> goodInBasket)
        {
            UserID = userID;
            BU = bU;
            Price = price;
            //GoodInBasketID = goodInBasketID;
            GoodInBasket = goodInBasket;
        }
    }
}
