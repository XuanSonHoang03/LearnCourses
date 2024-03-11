using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.EnrollmentRepo
{
    public interface IEnrollmentRepository
    {
        List<Course> GetAllCourseUserBuyed(int userId);
        List<Lesson> GetAllLessonUserBuyed(int userId);
        void AddEnrollment(Enrollment enrollment);
        void DeleteEnrollment(Enrollment enrollment);
    }
}
