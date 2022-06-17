using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2TypeDiabet.DBServices;
using WpfApp2TypeDiabet.Models;

namespace WpfApp2TypeDiabet.Services
{
    public class GoodBasketService
    {
        public GoodBasket GoodBasket { get; set; }
        public List<GoodBasket> Baskets { get; set; } = new List<GoodBasket>();
        public string CreateGoodBasket(int userID, double bU, double price)
        {
            GoodBasket = new GoodBasket(userID, bU, price);
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.GoodBasket.Add(GoodBasket);
                    db.SaveChanges();
                }
                return "Success";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
        public GoodBasket GetGoodBasket(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var goodBasket = from b in db.GoodBasket
                                 where b.id == id
                                 select b;
                GoodBasket = goodBasket.First();
            }
            return GoodBasket;
        }
    }
}
