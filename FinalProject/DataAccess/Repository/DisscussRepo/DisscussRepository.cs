using BusinessObject.Model;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.DisscussRepo
{
    public class DisscussRepository : IDisscussRepository
    {
        public bool AddDisscuss(Discussion diss)
        {
            return DisscussDAO.Instance.AddDisscuss(diss);
        }

        public bool DeleteDisscuss(int id)
        {
            return DisscussDAO.Instance.DeleteDisscuss(id);
        }

        public List<Discussion> getDiscussByUserId(int id)
        {
            return DisscussDAO.Instance.getDisCussByuserId(id);
        }

        public List<Discussion> GetDisscusses()
        {
            return DisscussDAO.Instance.GetDisscusses();
        }

        public bool UpdateDisscuss(Discussion diss)
        {
           return DisscussDAO.Instance.UpdateDisscuss(diss);
        }
    }
}
