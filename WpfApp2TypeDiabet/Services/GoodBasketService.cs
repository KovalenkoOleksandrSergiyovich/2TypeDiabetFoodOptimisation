using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2TypeDiabet.DBServices;
using WpfApp2TypeDiabet.Models;
using static WpfApp2TypeDiabet.Services.OptimizeService;

namespace WpfApp2TypeDiabet.Services
{
    public class GoodBasketService
    {
        public GoodBasket GoodBasket { get; set; }
        public GoodInBasket GoodInBasket { get; set; }
        //public ObservableCollection<Result> Baskets { get; set; } = new ObservableCollection<Result>();
        public ObservableCollection<GoodBasket> Baskets { get; set; } = new ObservableCollection<GoodBasket>();
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
        public ObservableCollection<GoodBasket> GetAllBaskets(User user)
        {
            if (Baskets.Any())
            {
                Baskets.Clear();
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = from goodBasket in db.GoodBasket
                             join goodInBasket in db.GoodInBasket on goodBasket.id equals goodInBasket.GoodBasketID
                             join users in db.Users on goodBasket.UserID equals users.id
                             where goodBasket.UserID == user.id
                             select new
                             {
                                 basketID = goodBasket.id,
                                 basketPrice = goodBasket.Price,
                                 basketBU = goodBasket.BU,
                                 basketUser = goodBasket.UserID
                             };
                foreach (var e in result)
                {
                    // resultRecord = new Result();
                    GoodBasket = new GoodBasket(e.basketUser, e.basketBU, e.basketPrice);
                    GoodBasket.id = e.basketID;
                    //resultRecord.BU = e.basketBU;
                    //resultRecord.Price = e.basketPrice;

                    //Baskets.Add(resultRecord);
                    Baskets.Add(GoodBasket);
                }
                return Baskets;
            }
        }
    }
}
