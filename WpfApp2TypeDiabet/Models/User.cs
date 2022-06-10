using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2TypeDiabet.DBServices;

namespace WpfApp2TypeDiabet.Models
{
    public class User
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string Gender { get; set; }
        public bool IsSuperUser { get; set; }

        public User(string userName, string password, int age, double height, double weight, string gender, bool isSuperUser)
        {
            UserName = userName;
            Password = password;
            Age = age;
            Height = height;
            Weight = weight;
            Gender = gender;
            IsSuperUser = isSuperUser;
        }


        //public ObservableCollection<Goods> CustomGoods { get; set; } = new ObservableCollection<Goods>();
        //public ObservableCollection<Restriction> CustomRestrictions { get; set; } = new ObservableCollection<Restriction>();
        //public ObservableCollection<GoodBasket> GoodBaskets { get; set; } = new ObservableCollection<GoodBasket>();
    }
}
