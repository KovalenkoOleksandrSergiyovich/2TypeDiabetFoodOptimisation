using System;
using System.Collections.ObjectModel;
using System.Linq;
using WpfApp2TypeDiabet.DBServices;
using WpfApp2TypeDiabet.Models;

namespace WpfApp2TypeDiabet.Services
{
    public class UserService
    {
        //властивість для отримання та встановлення значень поточного користувача
        public User CurrentUser { get; set; }
        //метод перевірки доступності імені в базі даних при створенні нового коритсувача
        public bool IsUserNameAvaliable(User newUser)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var users = from b in db.Users
                            where b.UserName.Equals(newUser.UserName)
                            select b;
                if (users.Any())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        //метод створення об'єкту класу "користувач"
        public void CreateUser(User newUser)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var users = from b in db.Users
                            orderby b.id
                            select b;
                newUser.id = users.Last().id + 1;
                db.Users.Add(newUser);
                db.SaveChanges();
            }
        }
        //метод обробки спроби авторизації
        public User AttemptToLogin(string userName, string password)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var user = from b in db.Users
                            where b.UserName.Equals(userName) && b.Password.Equals(password)
                            select b;
                if(user.Any())
                {
                    return user.First();
                }
                else
                {
                    return null;
                }
            }
        }
        //метод для виходу з облікового запису користувача
        public void LogOut()
        {
            CurrentUser = null;
        }
        //метод для оновлення даних про користувача в базі даних
        public string UpdateDBInfo(string username, string password, int age, double height, double weight, string gender)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var user = from b in db.Users
                           where b.id == CurrentUser.id
                           select b;
                if (!user.First().UserName.Equals(username))
                {
                    user.First().UserName = username;
                }
                if (!user.First().Password.Equals(password))
                {
                    user.First().Password = password;
                }
                if (user.First().Age != age)
                {
                    user.First().Age = age;
                }
                if (user.First().Height != height)
                {
                    user.First().Height = height;
                }
                if (user.First().Weight != weight)
                {
                    user.First().Weight = weight;
                }
                if (!user.First().Gender.Equals(gender))
                {
                    user.First().Gender = gender;
                }
                try
                {
                    db.SaveChanges();
                    CurrentUser = GetUser(CurrentUser.id);
                    return "Success";
                }
                catch(Exception e)
                {
                    return e.Message;
                }
            }
        }
        //метод для оновлення даних про користувача
        public void UpdateInfo(int age, double height, double weight, string gender)
        {
            CurrentUser.Age = age;
            CurrentUser.Height = height;
            CurrentUser.Weight = weight;
            CurrentUser.Gender = gender;
        }
        ////метод для отримання об'єкту класу "користувач" за ідентифікатором користувача
        public User GetUser(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var user = from b in db.Users
                           where b.id == id
                           select b;
                if (user.Any())
                {
                    return user.First();
                }
                else
                {
                    return null;
                }
            }
        }
        //метод для видалення об'єкту класу "користувач" з бази даних
        public string DeleteUser(User userToDelete)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    db.Users.Remove(userToDelete);
                    db.SaveChanges();
                    return "Success";
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
        }
        //метод для отримання списку користувачів
        public ObservableCollection<User> GetUsersList(User currentUser)
        {
            ObservableCollection<User> users = new ObservableCollection<User>();
            using (ApplicationContext db = new ApplicationContext())
            {
                var DbUsers = from b in db.Users
                              where b.id != currentUser.id
                              select b;
                foreach(User user in DbUsers)
                {
                    users.Add(user);
                }
            }
            return users;
        }
        //метод для отримання об'єкту класу "користувач" за іменем користувача
        public User GetUser(string Name)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var user = from b in db.Users
                           where b.UserName == Name
                           select b;
                if (user.Any())
                {
                    return user.First();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
