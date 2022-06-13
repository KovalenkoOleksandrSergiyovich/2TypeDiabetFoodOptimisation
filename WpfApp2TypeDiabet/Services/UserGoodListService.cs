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
    public class UserGoodListService
    {
        public UserGoodList UserGoodList { get; set; }
        public ObservableCollection<UserGood> UserGoods { get; set; } = new ObservableCollection<UserGoodListService.UserGood>();

        public class UserGood
        {
            public string GoodName { get; set; }
            public double GoodPrice { get; set; }
            public double GoodAmount { get; set; }
            public string GoodUnits { get; set; }
            public double GoodCarbohydrates { get; set; }
        }
        public string CreateUserGoodList(int userID, int goodInShopID)
        {
            try
            {
                UserGoodList = new UserGoodList() { UserID = userID, GoodInShopID = goodInShopID };
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.UserGoodList.Add(UserGoodList);
                    db.SaveChanges();
                }
                return "Success";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
        public string DeleteUserGoodList(int userID, int goodInShopID)
        {
            try
            {
                UserGoodList = new UserGoodList() { UserID = userID, GoodInShopID = goodInShopID };
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.UserGoodList.Remove(UserGoodList);
                    db.SaveChanges();
                }
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public ObservableCollection<UserGood> GetUserGoods(User user)
        {
            if(UserGoods.Any())
            {
                UserGoods.Clear();
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = from userGood in db.UserGoodList
                             join goodInShop in db.GoodInShop on userGood.GoodInShopID equals goodInShop.id
                             join good in db.Goods on goodInShop.GoodId equals good.id
                             join goodShopState in db.GoodShopState on goodInShop.id equals goodShopState.GoodInShopID
                             join goodState in db.GoodState on goodShopState.GoodStateID equals goodState.id
                             where userGood.UserID == user.id
                             select new
                             {
                                 GoodName = good.GoodName,
                                 GoodPrice = goodInShop.GoodPrice,
                                 GoodAmount = goodInShop.GoodAmount,
                                 GoodUnits = goodInShop.GoodUnits,
                                 GoodCarbohydrates = goodState.Carbohydrates
                             };
                foreach(var e in result)
                {
                    UserGood userGood = new UserGood()
                    {
                        GoodName = e.GoodName,
                        GoodPrice = e.GoodPrice,
                        GoodAmount = e.GoodAmount,
                        GoodUnits = e.GoodUnits,
                        GoodCarbohydrates = e.GoodCarbohydrates
                    };
                    UserGoods.Add(userGood);
                }
                return UserGoods;
            }
        }
        public ObservableCollection<string> GetUserGoodList(User user)
        {
            if (UserGoods.Any())
            {
                UserGoods.Clear();
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                ObservableCollection<string> goods = new ObservableCollection<string>();
                var result = from userGood in db.UserGoodList
                             join goodInShop in db.GoodInShop on userGood.GoodInShopID equals goodInShop.id
                             join good in db.Goods on goodInShop.GoodId equals good.id
                             where userGood.UserID == user.id
                             select good;
                foreach (var e in result)
                {
                    goods.Add(e.GoodName);
                }
                return goods;
            }
        }
        public ObservableCollection<string> GetStandartGoodList()
        {
            if (UserGoods.Any())
            {
                UserGoods.Clear();
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                ObservableCollection<string> goods = new ObservableCollection<string>();
                var result = (from good in db.Goods
                             join goodInShop in db.GoodInShop on good.id equals goodInShop.GoodId
                             where goodInShop.IsDefault == true
                             select good.GoodName).Distinct();
                foreach (var e in result)
                {
                    goods.Add(e);
                }
                return goods;
            }
        }
        public ObservableCollection<UserGood> GetStandartGoods()
        {
            if (UserGoods.Any())
            {
                UserGoods.Clear();
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = from goodInShop in db.GoodInShop
                             join good in db.Goods on goodInShop.GoodId equals good.id
                             join goodShopState in db.GoodShopState on goodInShop.id equals goodShopState.GoodInShopID
                             join goodState in db.GoodState on goodShopState.GoodStateID equals goodState.id
                             where goodInShop.IsDefault
                             select new
                             {
                                 GoodName = good.GoodName,
                                 GoodPrice = goodInShop.GoodPrice,
                                 GoodAmount = goodInShop.GoodAmount,
                                 GoodUnits = goodInShop.GoodUnits,
                                 GoodCarbohydrates = goodState.Carbohydrates
                             };
                foreach (var e in result)
                {
                    UserGood userGood = new UserGood()
                    {
                        GoodName = e.GoodName,
                        GoodPrice = e.GoodPrice,
                        GoodAmount = e.GoodAmount,
                        GoodUnits = e.GoodUnits,
                        GoodCarbohydrates = e.GoodCarbohydrates
                    };
                    UserGoods.Add(userGood);
                }
                return UserGoods;
            }
        }
    }
}
