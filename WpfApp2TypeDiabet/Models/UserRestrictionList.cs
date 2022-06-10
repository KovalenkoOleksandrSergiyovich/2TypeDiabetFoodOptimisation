using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2TypeDiabet.Models
{
    public class UserRestrictionList
    {
        public int id { get; set; }
        public int RestrictionID { get; set; }
        public Restriction Restriction { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
