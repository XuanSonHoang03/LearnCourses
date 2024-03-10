using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace DataAccess.Repository.RatingRepo
{
    public interface IRatingRepository
    {
        List<Rating> GetRatingByCourseId(int id);
        void AddRating(Rating rating);
        void UpdateRating(Rating rating);
        void DeleteRating(Rating rating);
        List<Rating> GetRatingByUserId(int id);
        List<Rating> GetRattingByCourseIdHaveName(int id);
    }
}
