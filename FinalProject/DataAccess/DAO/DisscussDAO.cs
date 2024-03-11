using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class DisscussDAO
    {
        public project_prn211Context context = new project_prn211Context();
        private static DisscussDAO instance;
        private static readonly object instanceLock = new object();
        public static DisscussDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new DisscussDAO();
                    }
                    return instance;
                }
            }
        }
        private DisscussDAO()
        {
        }
        public List<Discussion> GetDisscusses()
        {
            return context.Discussions.ToList();
        }
        public void AddDisscuss(Discussion diss)
        {
            try
            {
                context.Discussions.Add(diss);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Add Disscuss Failed " + e.Message);
            }
        }
        public bool DeleteDisscuss(int id)
        {
            try
            {
                Discussion diss = context.Discussions.Find(id);
                context.Discussions.Remove(diss);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateDisscuss(Discussion diss)
        {
            try
            {
                var existingDiscussion = context.Discussions.Find(diss.Id);
                if (existingDiscussion != null)
                {
                    existingDiscussion.Title = diss.Title;
                    existingDiscussion.UpdatedDate = DateTime.Now;
                    existingDiscussion.Content = diss.Content;

                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false; // id null
                }
            }
            catch (Exception e)
            {
                throw new Exception("Update Disscuss Failed " + e.Message);
            }
        }

        public Discussion GetDisscussById(int id)
        {
            return context.Discussions.Find(id);
        }
        public List<Discussion> getDisCussByuserId(int id)
        {
            return context.Discussions.Where(x => x.UserId == id).ToList();
        }
        public string GetNameByDisscussId(int id)
        {
            string sql = "select u.FirstName + u.Lastname as Name from Users u join Discussions d on u.Id = d.UserId where d.Id = " + id;
            return context.Database.ExecuteSqlRaw(sql).ToString();
        }
        public List<Discussion> GetNameOfUserByDiscuss()
        {
            /*select * from Discusstions d join Users u on u.Id = d.Userid*/
            var query = from d in context.Discussions
                        join u in context.Users on d.UserId equals u.Id
                        select new Discussion
                        {
                            User = u,
                            Title = d.Title,
                            Content = d.Content,
                            CreatedDate = d.CreatedDate,
                            UpdatedDate = d.UpdatedDate
                        };
            return query.ToList();
        }
    }
}
