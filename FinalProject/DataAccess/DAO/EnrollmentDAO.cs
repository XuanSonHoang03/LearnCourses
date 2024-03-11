using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class EnrollmentDAO
    {
        public project_prn211Context context = new project_prn211Context();
        private static EnrollmentDAO instance;
        private static readonly object instanceLock = new object();
        public static EnrollmentDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new EnrollmentDAO();
                    }
                    return instance;
                }
            }
        }
        private EnrollmentDAO() { }
        public List<Enrollment> GetEnrollmentByUserId(int id)
        {
            return context.Enrollments.Where(e => e.UserId == id).ToList();
        }
        public List<Enrollment> GetEnrollmentByCourseId(int id)
        {
            return context.Enrollments.Where(e => e.CourseId == id).ToList();
        }
        public void AddEnrollment(Enrollment enrollment)
        {
            context.Enrollments.Add(enrollment);
            context.SaveChanges();
        }
        public void UpdateEnrollment(Enrollment enrollment)
        {
            context.Enrollments.Update(enrollment);
            context.SaveChanges();
        }
        public void DeleteEnrollment(Enrollment enrollment)
        {
            context.Enrollments.Remove(enrollment);
            context.SaveChanges();
        }

        public List<Course> GetAllCoursessOfUserBuyed(int userId)
        {
            /*select c.* from Courses c
            join Enrollments e on e.CourseId = c.Id
            join Users u on u.Id = e.Id
            where e.UserId = 2*/
            var query = from c in context.Courses
                        join e in context.Enrollments on c.Id equals e.CourseId
                        join u in context.Users on e.UserId equals u.Id
                        where e.UserId == userId
                        select c;
            return query.ToList();
        }
        public List<Lesson> GetAllLessonUserBuyed(int courseId)
        {
            return context.Lessons.Where(l => l.CourseId == courseId).ToList();
        }

    }
}


/*select l.* from Lessons l 
join Courses c on c.Id = l.CourseId
join Enrollments e on e.CourseId = c.Id
join Users u on u.Id = e.Id
where u.Id = 1



select c.* from Courses c
join Enrollments e on e.CourseId = c.Id
join Users u on u.Id = e.Id
where e.UserId = 2*/