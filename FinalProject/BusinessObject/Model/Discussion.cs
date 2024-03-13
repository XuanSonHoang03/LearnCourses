using System;
using System.Collections.Generic;

namespace BusinessObject.Model
{
    public partial class Discussion
    {
        public Discussion()
        {
            UserDissucsses = new HashSet<UserDissucss>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<UserDissucss> UserDissucsses { get; set; }
    }
}
