using System;
using System.Collections.Generic;
using System.Linq;
using WpfApp2TypeDiabet.DBServices;
using WpfApp2TypeDiabet.Models;

namespace WpfApp2TypeDiabet.Services
{
    public class GoodInShopService
    {
        //властивість для отримання та встановлення значеннь про товар в магазині
        public GoodInShop GoodInShop { get; set; }
        //властивість для отримання та встановлення значень про список товарів в магазині
        public List<GoodInShop> Goods { get; set; }
        //метод для створення товару в магазині
        public string CreateGoodInShop(User user, int goodID, double goodPrice, double goodAmount, string? goodUnits, int restrictionID)
        {
            try
            {
                if (user.IsSuperUser)
                {
                    GoodInShop = new GoodInShop()
                    {
                        GoodId = goodID,
                        GoodPrice = goodPrice,
                        GoodAmount = goodAmount,
                        GoodUnits = goodUnits,
                        IsDefault = true, 
                        RestrictionID = restrictionID
                    };
                }
                else
                {
                    GoodInShop = new GoodInShop()
                    {
                        GoodId = goodID,
                        GoodPrice = goodPrice,
                        GoodAmount = goodAmount,
                        GoodUnits = goodUnits,
                        IsDefault = false,
                        RestrictionID = restrictionID
                    };
                }
                using (ApplicationContext db = new ApplicationContext())
                {
                    var goodInShop = from g in db.GoodInShop
                                       orderby g.id
                                       select g;
                    GoodInShop.id = goodInShop.Last().id + 1;
                    db.GoodInShop.Add(GoodInShop);
                    db.SaveChanges();
                }
                return "Success";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
        //метод для видалення товару з магазину
        public string DeleteGoodInShop()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach(GoodInShop goodInShop in Goods)
                    {
                        db.GoodInShop.Remove(goodInShop);
                        db.SaveChanges();
                    }
                }
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        //метод для отримання значень про товар в магазині за об'єктом товаром
        public void GetGoodInShop(Goods good)
        {
            Goods = new List<GoodInShop>();
            using (ApplicationContext db = new ApplicationContext())
            {
                var goodInShop = from b in db.GoodInShop
                                 where b.GoodId == good.id
                                 select b;
                foreach(GoodInShop product in goodInShop)
                {
                    Goods.Add(product);
                }
            }
        }
        //метод для отримання даних про товар в магазині за його ідентифіктатором
        public GoodInShop GetGoodInShop(int ID)
        {
            GoodInShop = new GoodInShop();
            using (ApplicationContext db = new ApplicationContext())
            {
                var goodInShop = from b in db.GoodInShop
                                 where b.id == ID
                                 select b;
                GoodInShop = goodInShop.First();
            }
            return GoodInShop;
        }
        //метод для отримання даних про товар за  ідентифікатором товару в магазині
        public Goods GetGoodByShopID(int goodInShopID)
        {
            Goods targetGood;
            using (ApplicationContext db = new ApplicationContext())
            {
                var goods = from b in db.GoodInShop
                           join good in db.Goods on b.GoodId equals good.id
                                 where b.id == goodInShopID
                           select good;
                targetGood = goods.First();
            }
            return targetGood;
        }
        //метод для отримання кількості хлібних одиниць товару з ідентифікатором товару в магазині
        public double GetGoodBUByShopID(int goodInShopID)
        {
            double BU;
            using (ApplicationContext db = new ApplicationContext())
            {
                var BUs = from b in db.GoodInShop
                            join goodShopState in db.GoodShopState on b.id equals goodShopState.GoodInShopID
                            join goodState in db.GoodState on goodShopState.GoodStateID equals goodState.id
                            where b.id == goodInShopID
                            select goodState.Carbohydrates;
                BU = BUs.First();
            }
            return BU;
        }
    }
}
