using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2TypeDiabet.DBServices;
using WpfApp2TypeDiabet.Models;

namespace WpfApp2TypeDiabet.Services
{
    public class GoodInShopService
    {
        public GoodInShop GoodInShop { get; set; }
        public List<GoodInShop> Goods { get; set; }
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
