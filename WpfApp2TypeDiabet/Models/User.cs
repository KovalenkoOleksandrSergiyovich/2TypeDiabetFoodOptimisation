using System.Collections.ObjectModel;

namespace WpfApp2TypeDiabet.Models
{
    public class User
    {
        //властивість для встановлення та отримання значень ідентифікатора користувача
        public int id { get; set; }
        //властивість для встановлення та отримання значень імені користувача
        public string UserName { get; set; }
        //властивість для встановлення та отримання значень паролю користувача
        public string Password { get; set; }
        //властивість для встановлення та отримання значень віку користувача
        public int Age { get; set; }
        //властивість для встановлення та отримання значень зрісту користувача
        public double Height { get; set; }
        //властивість для встановлення та отримання значень ваги користувача
        public double Weight { get; set; }
        //властивість для встановлення та отримання значень статі користувача
        public string Gender { get; set; }
        //властивість для встановлення та отримання значень показника, чи є користувач адміністратором
        public bool IsSuperUser { get; set; }
        //конструктор з параметрами класу
        public User(string userName, string password, int age, double height, double weight, 
            string gender, bool isSuperUser)
        {
            UserName = userName;
            Password = password;
            Age = age;
            Height = height;
            Weight = weight;
            Gender = gender;
            IsSuperUser = isSuperUser;
        }
        //властивість для встановлення та отримання списку товарі користувача
        public ObservableCollection<UserGoodList> CustomGoods { get; set; } = new ObservableCollection<UserGoodList>();
        //властивість для встановлення та отримання списку обмежень користувача
        public ObservableCollection<UserRestrictionList> CustomRestrictions { get; set; } = new ObservableCollection<UserRestrictionList>();
        //властивість для встановлення та отримання списку продуктових кошиків користувача
        public ObservableCollection<GoodBasket> GoodBaskets { get; set; } = new ObservableCollection<GoodBasket>();
    }
}
