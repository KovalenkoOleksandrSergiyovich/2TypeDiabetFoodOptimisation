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
        public ObservableCollection<UserRestriction> UserRestrictionList { get; set; } = new ObservableCollection<RestrictionService.UserRestriction>();
        public class UserRestriction
        {
            public string GoodName { get; set; }
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
        public ObservableCollection<UserRestriction> GetUserRestrictions(User user)
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
                             join good in db.Goods on goodInShop.GoodId equals good.id
                             where userRestriction.UserID == user.id
                             select new
                             {
                                 GoodName = good.GoodName,
                                 RestrictionName = restriction.RestrictionName,
                                 RestrictionComparator = restriction.Comparator,
                                 RestrictionAmount = restriction.Amount,
                                 RestrictionUnit = restriction.Unit
                             };
                foreach (var r in result)
                {
                    UserRestriction userRestriction = new UserRestriction()
                    {
                        GoodName = r.GoodName,
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
        public ObservableCollection<UserRestriction> GetStandartRestrictions()
        {
            if (UserRestrictionList.Any())
            {
                UserRestrictionList.Clear();
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = from  restriction in db.Restriction
                             join goodInShop in db.GoodInShop on restriction.id equals goodInShop.RestrictionID
                             join good in db.Goods on goodInShop.GoodId equals good.id
                             where restriction.IsDefault && goodInShop.IsDefault
                             select new
                             {
                                 GoodName = good.GoodName,
                                 RestrictionName = restriction.RestrictionName,
                                 RestrictionComparator = restriction.Comparator,
                                 RestrictionAmount = restriction.Amount,
                                 RestrictionUnit = restriction.Unit
                             };
                foreach (var r in result)
                {
                    UserRestriction userRestriction = new UserRestriction()
                    {
                        GoodName = r.GoodName,
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
                        result = "Більше або дорівнює ніж";
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
                case "Більше або дорівнює ніж":
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
