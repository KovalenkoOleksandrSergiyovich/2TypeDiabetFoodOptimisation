using System;
using System.Collections.Generic;
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
    }
}
