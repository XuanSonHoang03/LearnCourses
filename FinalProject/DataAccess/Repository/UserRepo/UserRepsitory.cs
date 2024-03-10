using BusinessObject.Model;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.UserRepo
{
    public class UserRepsitory : IUserRepository
    {
        public void AddUser(User user)
        {
            UserDAO.Instance.AddUser(user); 
        }

        public bool checkAdminAccount(string username, string password)
        {
            return UserDAO.Instance.checkAdminAccount(username, password);
        }

        public void DeleteUser(User user)
        {
            UserDAO.Instance.DeleteUser(user);
        }

        public User GetUserById(int id)
        {
            return UserDAO.Instance.GetUserById(id);
        }

        public User GetUserByUserPass(string username, string password)
        {
            return UserDAO.Instance.GetUserByEmailPasword(username, password);
        }

        public List<User> GetUsers()
        {
            return UserDAO.Instance.GetUsers();
        }

        public void UpdateUser(User user)
        {
            UserDAO.Instance.UpdateUser(user);
        }
    }
}
