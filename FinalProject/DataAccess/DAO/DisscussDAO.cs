using BusinessObject.Model;
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
        public bool AddDisscuss(Discussion diss)
        {
            try
            {
                context.Discussions.Add(diss);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Add Disscuss Failed " + e.Message);
                return false;
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
                Discussion dissOld = context.Discussions.Find(diss.Id);
                dissOld.Content = diss.Content;
                dissOld.CreatedDate = diss.CreatedDate;
                dissOld.UserId = diss.UserId;
                dissOld.UpdatedDate = DateTime.Now;
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
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
    }
}
