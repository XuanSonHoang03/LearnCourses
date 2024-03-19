using BusinessObject.Model;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.UserDiscussRepo
{
    public class UserDiscussRepository : IUserDiscussRepository
    {
        public void AddUserDiscuss(UserDissucss userDiscuss)
        {
            UserDiscussDAO.Instance.AddUserDiscuss(userDiscuss);
        }

        public void DeleteUserDiscuss(UserDissucss userDiscuss)
        {
            UserDiscussDAO.Instance.DeleteUserDiscuss(userDiscuss);
        }

        public List<UserDissucss> GetAllDiscuss(int id)
        {
            return UserDiscussDAO.Instance.getAllCommentByDiscussId(id);
        }

        public void UpdateUserDiscuss(UserDissucss userDiscuss)
        {
            UserDiscussDAO.Instance.UpdateUserDiscuss(userDiscuss);
        }
        public List<UserDissucss> getAllCommentByDiscussId(int id)
        {
            return UserDiscussDAO.Instance.getAllCommentByDiscussId(id);
        }
    }
}
