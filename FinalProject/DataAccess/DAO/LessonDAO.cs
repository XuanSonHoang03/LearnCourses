using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class LessonDAO
    {
        public project_prn211Context context = new project_prn211Context();

        private static LessonDAO instance;
        private static readonly object instanceLock = new object();

        private LessonDAO() { }
        public static LessonDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new LessonDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Lesson> GetLessonById(int id)
        {
            return context.Lessons.Where(c => c.Id == id).ToList();
        }
        public IEnumerable<Lesson> GetLessons()
        {
            return context.Lessons.ToList();
        }
        public void AddLesson(Lesson lesson)
        {
            context.Lessons.Add(lesson);
            context.SaveChanges();
        }
        public List<Lesson> GetLessonByCourseID(int courseId)
        {
            return context.Lessons.Where(c => c.CourseId == courseId).ToList();
        }
        public void UpdateLesson(Lesson lesson)
        {
            context.Lessons.Update(lesson);
            context.SaveChanges();
        }
        public void DeleteLesson(Lesson lesson)
        {
            context.Lessons.Remove(lesson);
            context.SaveChanges();
        }
    }
}
