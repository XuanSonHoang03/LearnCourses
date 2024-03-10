using BusinessObject.Model;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.RatingRepo
{
    public class RatingRepository : IRatingRepository
    {
        public void AddRating(Rating rating)
        {
            RatingDAO.Instance.AddRating(rating);
        }

        public void DeleteRating(Rating rating)
        {
            RatingDAO.Instance.DeleteRating(rating);
        }

        public List<Rating> GetRatingByCourseId(int id)
        {
            return RatingDAO.Instance.GetRatingByCourseId(id);
        }

        public List<Rating> GetRatingByUserId(int id)
        {
            return RatingDAO.Instance.GetRatingByUserId(id);
        }

        public void UpdateRating(Rating rating)
        {
            RatingDAO.Instance.UpdateRating(rating);
        }
        public List<Rating> GetRattingByCourseIdHaveName(int id)
        {
            return RatingDAO.Instance.GetRattingByCourseIdHaveName(id);
        }
    }
}
