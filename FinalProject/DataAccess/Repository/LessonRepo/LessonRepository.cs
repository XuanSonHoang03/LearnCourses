using BusinessObject.Model;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.LessonRepo
{
    public class LessonRepository : ILessonRepostory
    {
        public void AddLesson(Lesson lesson)
        {
            LessonDAO.Instance.AddLesson(lesson);
        }

        public void DeleteLesson(Lesson lesson)
        {
            LessonDAO.Instance.DeleteLesson(lesson);
        }

        public List<Lesson> GetLessonById(int id)
        {
            return LessonDAO.Instance.GetLessonById(id);
        }

        public List<Lesson> GetLessonByCourseId(int id)
        {
            return LessonDAO.Instance.GetLessonByCourseID(id);
        }

        public IEnumerable<Lesson> GetLessons()
        {
            return LessonDAO.Instance.GetLessons();
        }

        public void UpdateLesson(Lesson lesson)
        {
            LessonDAO.Instance.UpdateLesson(lesson);
        }
    }
}
