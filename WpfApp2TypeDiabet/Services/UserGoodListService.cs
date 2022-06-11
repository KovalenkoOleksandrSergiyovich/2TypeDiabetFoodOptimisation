using System;
using System.Collections.Generic;
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
        public string CreateUserGoodList(int userID, int goodInShopID)
        {
            try
            {
                UserGoodList = new UserGoodList() { UserID = userID, GoodID = goodInShopID };
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
    }
}
