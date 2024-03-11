using BusinessObject.Model;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.EnrollmentRepo
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        public void AddEnrollment(Enrollment enrollment)
        {
            EnrollmentDAO.Instance.AddEnrollment(enrollment);
        }

        public void DeleteEnrollment(Enrollment enrollment)
        {
            EnrollmentDAO.Instance.DeleteEnrollment(enrollment);
        }

        public List<Course> GetAllCourseUserBuyed(int userId)
        {
            return EnrollmentDAO.Instance.GetAllCoursessOfUserBuyed(userId);
        }

        public List<Lesson> GetAllLessonUserBuyed(int userId)
        {
            return EnrollmentDAO.Instance.GetAllLessonUserBuyed(userId);
        }
    }
}
