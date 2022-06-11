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
    }
}
