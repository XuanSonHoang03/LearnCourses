using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.LessonRepo
{
    public interface ILessonRepostory
    {
        public List<Lesson> GetLessonById(int id);
        public List<Lesson> GetLessonByCourseId(int id);
        public IEnumerable<Lesson> GetLessons();
        public void AddLesson(Lesson lesson);
        public void UpdateLesson(Lesson lesson);
        public void DeleteLesson(Lesson lesson);
    }
}
