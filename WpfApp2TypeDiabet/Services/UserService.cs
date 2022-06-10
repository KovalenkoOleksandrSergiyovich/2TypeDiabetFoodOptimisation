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
    public class UserService
    {
        public User CurrentUser { get; set; }
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
        public void UpdateInfo(int age, double height, double weight, string gender)
        {
            CurrentUser.Age = age;
            CurrentUser.Height = height;
            CurrentUser.Weight = weight;
            CurrentUser.Gender = gender;
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
        public ObservableCollection<User> GetUsersList()
        {
            ObservableCollection<User> users = new ObservableCollection<User>();
            using (ApplicationContext db = new ApplicationContext())
            {
                var DbUsers = from b in db.Users
                              select b;
                foreach(User user in DbUsers)
                {
                    users.Add(user);
                }
            }
            return users;
        }
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
        //public int GetUserGoodsCount(User user)
        //{
        //    int goodsCount;
        //    using (ApplicationContext db = new ApplicationContext())
        //    {
        //        var DbUsers = from b in db.Users
        //                      select b;
        //        foreach (User user in DbUsers)
        //        {
        //            users.Add(user);
        //        }
        //    }
        //}
        //public int GetUserRestrictionsCount(User user)
        //{
        //    int restrictionsCount;
        //    using (ApplicationContext db = new ApplicationContext())
        //    {
        //        var DbUsers = from b in db.Users
        //                      select b;
        //        foreach (User user in DbUsers)
        //        {
        //            users.Add(user);
        //        }
        //    }
        //}
        //public int GetUserBasketsCount(User user)
        //{
        //    int basketsCount;
        //    using (ApplicationContext db = new ApplicationContext())
        //    {
        //        var DbUsers = from b in db.Users
        //                      select b;
        //        foreach (User user in DbUsers)
        //        {
        //            users.Add(user);
        //        }
        //    }
        //}
    }
}
