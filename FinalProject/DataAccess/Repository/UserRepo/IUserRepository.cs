using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.UserRepo
{
    public interface IUserRepository
    {
        public bool checkAdminAccount(string username, string password);
        public User GetUserById(int id);
        public List<User> GetUsers();
        public void AddUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(User user);
        public User GetUserByUserPass(string username, string password);
    }
}
