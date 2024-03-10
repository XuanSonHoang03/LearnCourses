using System;
using System.Collections.Generic;

namespace BusinessObject.Model
{
    public partial class Course
    {
        public Course()
        {
            Enrollments = new HashSet<Enrollment>();
            Lessons = new HashSet<Lesson>();
            Orders = new HashSet<Order>();
            Ratings = new HashSet<Rating>();
            TransactionsHistories = new HashSet<TransactionsHistory>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int TeacherId { get; set; }
        public string ThumbnailImage { get; set; } = null!;
        public decimal Price { get; set; }
        public bool? IsPublished { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<TransactionsHistory> TransactionsHistories { get; set; }
    }
}
