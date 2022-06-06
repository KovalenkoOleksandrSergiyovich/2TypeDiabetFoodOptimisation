using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2TypeDiabet.DBServices;
using WpfApp2TypeDiabet.Models;

namespace WpfApp2TypeDiabet.Services
{
    public class UserService
    {
        public User CurrentUser { get; set; }
        public bool IsUserNameAvaliable(User newUser)
        {
            //ObservableCollection<User> users = new ObservableCollection<User>();
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

        public void CreateUser(User newUser)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.Add(newUser);
                db.SaveChanges();
            }
        }

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
        public void LogOut()
        {
            CurrentUser = null;
        }
        public string UpdateInfo(string username, string password, int age, double height, double weight, string gender)
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
    }
}
