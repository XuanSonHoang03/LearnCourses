using System;
using System.Collections.Generic;

namespace BusinessObject.Model
{
    public partial class User
    {
        public User()
        {
            Discussions = new HashSet<Discussion>();
            Enrollments = new HashSet<Enrollment>();
            Orders = new HashSet<Order>();
            Ratings = new HashSet<Rating>();
            TransactionsHistories = new HashSet<TransactionsHistory>();
            UserDissucsses = new HashSet<UserDissucss>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public decimal? Balance { get; set; }
        public int Role { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int IsDeleted { get; set; }

        public virtual ICollection<Discussion> Discussions { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<TransactionsHistory> TransactionsHistories { get; set; }
        public virtual ICollection<UserDissucss> UserDissucsses { get; set; }
    }
}
