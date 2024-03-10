using BusinessObject.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class UserDAO
    {
        public project_prn211Context context = new project_prn211Context();

        private static UserDAO instance;
        private static readonly object instanceLock = new object();

        private UserDAO(){}
        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }
        public bool checkAdminAccount(string username, string password)
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string adminEmail = configurationRoot["admin:username"];
            string adminPassword = configurationRoot["admin:password"];
            if (username == adminEmail && password == adminPassword)
            {
                return true;
            }
            return false;
        }

        public User GetUserById(int id)
        {
            return context.Users.Where(u => u.Id == id).FirstOrDefault();
        }
        public List<User> GetUsers()
        {
            return context.Users.ToList();
        }
        public void AddUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
        public void UpdateUser(User user)
        {
            if(user == null)
            {
                throw new Exception("User is null");
            }
            context.Users.Update(user);
            context.SaveChanges();
        }
        public void DeleteUser(User user)
        {
            context.Users.Remove(user);
            context.SaveChanges();
        }
        public User GetUserByEmailPasword(string email, string password)
        {
            return context.Users.Where(u => u.Username == email && u.Password == password).FirstOrDefault();
        }
    }
}
