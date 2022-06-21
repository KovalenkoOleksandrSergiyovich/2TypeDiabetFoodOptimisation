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
    public class UserRestrictionListService
    {
        public UserRestrictionList UserRestrictionList { get; set; }
        //public ObservableCollection<UserGood> UserGoods { get; set; } = new ObservableCollection<UserGoodListService.UserGood>();
        public string CreateUserRestrictionList(int userID, int restrictionID)
        {
            try
            {
                UserRestrictionList = new UserRestrictionList() { UserID = userID, RestrictionID = restrictionID };
                using (ApplicationContext db = new ApplicationContext())
                {
                    var userRestrictions = from g in db.UserRestrictionList
                                    orderby g.id
                                    select g;
                    if (userRestrictions.Any())
                    {
                        UserRestrictionList.id = userRestrictions.Last().id + 1;
                    }
                    else
                    {
                        UserRestrictionList.id = 1;
                    }
                    
                    db.UserRestrictionList.Add(UserRestrictionList);
                    db.SaveChanges();
                }
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
