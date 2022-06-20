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
    public class GoodInBasketService
    {
        public GoodInBasket GoodInBasket { get; set; }
        public ObservableCollection<GoodInBasket> GoodsInBassket { get; set; } = new ObservableCollection<GoodInBasket>();
        public string CreateGoodInBasket(ObservableCollection<GoodInBasket> goodInBasket)
        {
            string result = null;
            foreach(var g in goodInBasket)
            {
                try
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.GoodInBasket.Add(g);
                        db.SaveChanges();
                    }
                    result = "Success";
                }
                catch (Exception e)
                {
                    result = e.Message;
                }
            }
            return result;
        }
        public ObservableCollection<GoodInBasket> GetGoodInBaskets(GoodBasket goodBasket)
        {
            if(GoodsInBassket.Any())
            {
                GoodsInBassket.Clear();
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                var goodsInBasket = from goodInBasket in db.GoodInBasket
                                    join basket in db.GoodBasket on goodInBasket.GoodBasketID equals basket.id
                                    join goodInShop in db.GoodInShop on goodInBasket.GoodInShopID equals goodInShop.id
                                    where basket.id == goodBasket.id
                                    select new
                                    {
                                        goodInShopID = goodInShop.id,
                                        goodInBasketAmount = goodInBasket.Amount,
                                    };
                foreach (var g in goodsInBasket)
                {
                    //ProductBasket pb = new ProductBasket();
                    //pb.Amount = g.goodInBasketAmount;
                    //pb.GoodInShopID = g.goodInShopID;
                    GoodInBasket = new GoodInBasket(g.goodInShopID, g.goodInBasketAmount);
                    GoodsInBassket.Add(GoodInBasket);
                }
            }
            return GoodsInBassket;
        }
    }
}
