using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class RatingDAO
    {
        public project_prn211Context context = new project_prn211Context();
        
        private static RatingDAO instance;
        private static readonly object instanceLock = new object();
        public static RatingDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RatingDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Rating> GetRatingByCourseId(int id)
        {
            return context.Ratings.Where(r => r.CourseId == id).ToList();
        }
        public void AddRating(Rating rating)
        {
            context.Ratings.Add(rating);
            context.SaveChanges();
        }
        public void UpdateRating(Rating rating)
        {
            context.Ratings.Update(rating);
            context.SaveChanges();
        }
        public void DeleteRating(Rating rating)
        {
            context.Ratings.Remove(rating);
            context.SaveChanges();
        }
        public List<Rating> GetRatingByUserId(int id)
        {
            return context.Ratings.Where(r => r.UserId == id).ToList();
        }
        public List<Rating> GetRattingByCourseIdHaveName(int courseId)
        {
            /*select u.FirstName + u.LastName as 'Name', r.Rating, r.Comment, r.CreatedDate, r.UpdatedDate from Ratings r
            join Users u on u.Id = r.UserId
            where CourseId = 1*/
            var query = from r in context.Ratings
                        join u in context.Users on r.UserId equals u.Id
                        where r.CourseId == courseId
                        select new Rating
                        {
                            User = u,
                            Rating1 = r.Rating1,
                            Comment = r.Comment,
                            CreatedDate = r.CreatedDate,
                            UpdatedDate = r.UpdatedDate
                        };

            return query.ToList();
        }
    }
}
