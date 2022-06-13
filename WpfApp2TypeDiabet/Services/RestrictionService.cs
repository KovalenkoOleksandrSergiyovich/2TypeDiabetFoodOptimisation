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
    public class RestrictionService
    {
        public Restriction Restriction { get; set; }
        public ObservableCollection<UserGoodRestriction> UserGoodRestrictionList { get; set; } = new ObservableCollection<RestrictionService.UserGoodRestriction>();
        public ObservableCollection<UserRestriction> UserRestrictionList { get; set; } = new ObservableCollection<RestrictionService.UserRestriction>();
        public class UserGoodRestriction
        {
            public int RestrictionID { get; set; }
            public string GoodName { get; set; }
            public string RestrictionName { get; set; }
            public string RestrictionComparator { get; set; }
            public double RestrictionAmount { get; set; }
            public string RestrictionUnit { get; set; }
        }
        public class UserRestriction
        {
            public int RestrictionID { get; set; }
            public string RestrictionName { get; set; }
            public string RestrictionComparator { get; set; }
            public double RestrictionAmount { get; set; }
            public string RestrictionUnit { get; set; }
        }
        public string CreateRestriction(User user, string name, string comparator, double amount, string unit)
        {
            
            try
            {
                if(user.IsSuperUser)
                {
                    Restriction = new Restriction()
                    {
                        RestrictionName = name,
                        Comparator = comparator,
                        Amount = amount,
                        Unit = unit,
                        IsDefault = true
                    };
                }
                else
                {
                    Restriction = new Restriction()
                    {
                        RestrictionName = name,
                        Comparator = comparator,
                        Amount = amount,
                        Unit = unit,
                        IsDefault = false
                    };
                }
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Restriction.Add(Restriction);
                    db.SaveChanges();
                }
                return "Success";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
        public Restriction GetRestriction(int restrictionID)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var restr = from b in db.Restriction
                            where b.id == restrictionID
                            select b;
                Restriction = restr.First();
            }
            return Restriction;
        }
        public string DeleteRestriction(Restriction restriction)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Restriction.Remove(restriction);
                    db.SaveChanges();
                }
                return "Success";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
        public ObservableCollection<UserGoodRestriction> GetUserGoodRestrictions(User user)
        {
            if (UserGoodRestrictionList.Any())
            {
                UserGoodRestrictionList.Clear();
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = from userRestriction in db.UserRestrictionList
                             join restriction in db.Restriction on userRestriction.RestrictionID equals restriction.id
                             join goodInShop in db.GoodInShop on restriction.id equals goodInShop.RestrictionID
                             join good in db.Goods on goodInShop.GoodId equals good.id
                             where userRestriction.UserID == user.id
                             select new
                             {
                                 RestrictionID = restriction.id,
                                 GoodName = good.GoodName,
                                 RestrictionName = restriction.RestrictionName,
                                 RestrictionComparator = restriction.Comparator,
                                 RestrictionAmount = restriction.Amount,
                                 RestrictionUnit = restriction.Unit
                             };
                foreach (var r in result)
                {
                    UserGoodRestriction userRestriction = new UserGoodRestriction()
                    {
                        RestrictionID = r.RestrictionID,
                        GoodName = r.GoodName,
                        RestrictionName = r.RestrictionName,
                        RestrictionComparator = ComparatorToString(r.RestrictionComparator),
                        RestrictionAmount = r.RestrictionAmount,
                        RestrictionUnit = r.RestrictionUnit
                    };

                    UserGoodRestrictionList.Add(userRestriction);
                }
            }
            return UserGoodRestrictionList;
        }
        public ObservableCollection<UserGoodRestriction> GetStandartGoodRestrictions()
        {
            if (UserGoodRestrictionList.Any())
            {
                UserGoodRestrictionList.Clear();
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = from  restriction in db.Restriction
                             join goodInShop in db.GoodInShop on restriction.id equals goodInShop.RestrictionID
                             join good in db.Goods on goodInShop.GoodId equals good.id
                             where restriction.IsDefault && goodInShop.IsDefault
                             select new
                             {
                                 RestrictionID = restriction.id,
                                 GoodName = good.GoodName,
                                 RestrictionName = restriction.RestrictionName,
                                 RestrictionComparator = restriction.Comparator,
                                 RestrictionAmount = restriction.Amount,
                                 RestrictionUnit = restriction.Unit
                             };
                foreach (var r in result)
                {
                    UserGoodRestriction userRestriction = new UserGoodRestriction()
                    {
                        RestrictionID = r.RestrictionID,
                        GoodName = r.GoodName,
                        RestrictionName = r.RestrictionName,
                        RestrictionComparator = ComparatorToString(r.RestrictionComparator),
                        RestrictionAmount = r.RestrictionAmount,
                        RestrictionUnit = r.RestrictionUnit
                    };

                    UserGoodRestrictionList.Add(userRestriction);
                }
            }
            return UserGoodRestrictionList;
        }
        public ObservableCollection<UserRestriction> GetUserRestrictions(User user, Goods good)
        {
            if (UserRestrictionList.Any())
            {
                UserRestrictionList.Clear();
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = from userRestriction in db.UserRestrictionList
                             join restriction in db.Restriction on userRestriction.RestrictionID equals restriction.id
                             join goodInShop in db.GoodInShop on restriction.id equals goodInShop.RestrictionID
                             where userRestriction.UserID == user.id && goodInShop.GoodId == good.id
                             select new
                             {
                                 RestrictionID = restriction.id,
                                 RestrictionName = restriction.RestrictionName,
                                 RestrictionComparator = restriction.Comparator,
                                 RestrictionAmount = restriction.Amount,
                                 RestrictionUnit = restriction.Unit
                             };
                foreach (var r in result)
                {
                    UserRestriction userRestriction = new UserRestriction()
                    {
                        RestrictionID = r.RestrictionID,
                        RestrictionName = r.RestrictionName,
                        RestrictionComparator = ComparatorToString(r.RestrictionComparator),
                        RestrictionAmount = r.RestrictionAmount,
                        RestrictionUnit = r.RestrictionUnit
                    };

                    UserRestrictionList.Add(userRestriction);
                }
            }
            return UserRestrictionList;
        }
        public ObservableCollection<UserRestriction> GetStandartRestrictions(Goods good)
        {
            if (UserRestrictionList.Any())
            {
                UserRestrictionList.Clear();
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = from restriction in db.Restriction
                             join goodInShop in db.GoodInShop on restriction.id equals goodInShop.RestrictionID
                             where restriction.IsDefault && goodInShop.IsDefault && goodInShop.GoodId == good.id
                             select new
                             {
                                 RestrictionID = restriction.id,
                                 RestrictionName = restriction.RestrictionName,
                                 RestrictionComparator = restriction.Comparator,
                                 RestrictionAmount = restriction.Amount,
                                 RestrictionUnit = restriction.Unit
                             };
                foreach (var r in result)
                {
                    UserRestriction userRestriction = new UserRestriction()
                    {
                        RestrictionID = r.RestrictionID,
                        RestrictionName = r.RestrictionName,
                        RestrictionComparator = ComparatorToString(r.RestrictionComparator),
                        RestrictionAmount = r.RestrictionAmount,
                        RestrictionUnit = r.RestrictionUnit
                    };

                    UserRestrictionList.Add(userRestriction);
                }
            }
            return UserRestrictionList;
        }
        public string ComparatorToString(string comparator)
        {
            string result = null;
            switch(comparator)
            {
                case "ls":
                    {
                        result = "Менше ніж";
                        break;
                    }
                case "gr":
                    {
                        result = "Більше ніж";
                        break;
                    }
                case "lse":
                    {
                        result = "Менше або дорівнює";
                        break;
                    }
                case "gre":
                    {
                        result = "Більше або дорівнює";
                        break;
                    }
                case "eq":
                    {
                        result = "Дорівнює";
                        break;
                    }
            }
            return result;
        }
        public string ComparatorFromString(string String)
        {
            string result = null;
            switch (String)
            {
                case "Менше ніж":
                    {
                        result = "ls";
                        break;
                    }
                case "Більше ніж":
                    {
                        result = "gr";
                        break;
                    }
                case "Менше або дорівнює":
                    {
                        result = "lse";
                        break;
                    }
                case "Більше або дорівнює":
                    {
                        result = "gre";
                        break;
                    }
                case "Дорівнює":
                    {
                        result = "eq";
                        break;
                    }
            }
            return result;
        }
    }
}
