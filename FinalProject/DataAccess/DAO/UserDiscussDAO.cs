using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace DataAccess.DAO
{
    public class UserDiscussDAO
    {
        private static UserDiscussDAO instance;
        private UserDiscussDAO() { }
        private static readonly object instanceLock = new object();
        public static UserDiscussDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDiscussDAO();
                    }
                    return instance;
                }
            }
        }
        public List<UserDissucss> getAllCommentByDiscussId(int id) 
        { 
            using (var context = new project_prn211Context())
            {
                return context.UserDissucsses.Where(m => m.DisscussId == id).ToList();
            }
        }
        public void AddUserDiscuss(UserDissucss userDiscuss)
        {
            using (var context = new project_prn211Context())
            {
                context.UserDissucsses.Add(userDiscuss);
                context.SaveChanges();
            }
        }
        public void DeleteUserDiscuss(UserDissucss userDiscuss)
        {
            using (var context = new project_prn211Context())
            {
                context.UserDissucsses.Remove(userDiscuss);
                context.SaveChanges();
            }
        }
        public void UpdateUserDiscuss(UserDissucss userDiscuss)
        {
            using (var context = new project_prn211Context())
            {
                context.UserDissucsses.Update(userDiscuss);
                context.SaveChanges();
            }
        }
    }
}
