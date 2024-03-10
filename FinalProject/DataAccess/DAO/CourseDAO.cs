using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CourseDAO
    {
        public project_prn211Context context = new project_prn211Context();

        private static CourseDAO instance;
        private static readonly object instanceLock = new object();

        private CourseDAO() { }
        public static CourseDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CourseDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Course> GetCourseById(int id)
        {
            return context.Courses.Where(c => c.Id == id).ToList();
        }
        public IEnumerable<Course> GetCourses()
        {
            return context.Courses.ToList();
        }
        public void AddCourse(Course course)
        {
            context.Courses.Add(course);
            context.SaveChanges();
        }
        public void UpdateCourse(Course course)
        {
            context.Courses.Update(course);
            context.SaveChanges();
        }
        public void DeleteCourse(Course course)
        {
            context.Courses.Remove(course);
            context.SaveChanges();
        }
        public List<Course> GetCoursesByUserID(int id)
        {
            var query = from c in context.Courses
                        join o in context.Orders on c.Id equals o.CourseId
                        join u in context.Users on o.UserId equals u.Id
                        where u.Id == id
                        select c;
            return query.ToList();
        }
    }
}
