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
        void AddDisscuss(Discussion diss);
        bool DeleteDisscuss(int id);
        bool UpdateDisscuss(Discussion diss);
        Discussion getDisscussById(int id);
        string getNameOfUser(int id);
        List<Discussion> getDiscussByUserId(int id);
        List<Discussion> getNameOfUserDiscuss();
    }
}
