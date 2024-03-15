using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.UserDiscussRepo
{
    public interface IUserDiscussRepository
    {
        List<UserDissucss> GetAllDiscuss(int DiscussId);
        void AddUserDiscuss(UserDissucss userDiscuss);
        void DeleteUserDiscuss(UserDissucss userDiscuss);
        void UpdateUserDiscuss(UserDissucss userDiscuss);


    }
}
