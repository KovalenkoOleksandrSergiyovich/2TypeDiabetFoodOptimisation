using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2TypeDiabet.DBServices;
using WpfApp2TypeDiabet.Models;

namespace WpfApp2TypeDiabet.Services
{
    public class GoodShopStateService
    {
        public GoodShopState GoodShopState { get; set; }
        public string CreateGoodShopState(int goodInShopID, int goodStateID)
        {
            try
            {
                GoodShopState = new GoodShopState() { GoodInShopID = goodInShopID, GoodStateID = goodStateID };
                using (ApplicationContext db = new ApplicationContext())
                {
                    var shopStates = from g in db.GoodShopState
                                 orderby g.id
                                 select g;
                    if (shopStates.Any())
                    {
                        GoodShopState.id = shopStates.Last().id + 1;
                    }
                    else
                    {
                        GoodShopState.id = 1;
                    } 
                    db.GoodShopState.Add(GoodShopState);
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
