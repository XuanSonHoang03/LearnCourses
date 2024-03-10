using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace DataAccess.Repository.DisscussRepo
{
    public interface IDisscussRepository
    {
        List<Discussion> GetDisscusses();
        bool AddDisscuss(Discussion diss);
        bool DeleteDisscuss(int id);
        bool UpdateDisscuss(Discussion diss);
        List<Discussion> getDiscussByUserId(int id); 
    }
}
