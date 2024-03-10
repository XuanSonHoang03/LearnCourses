using System;
using System.Collections.Generic;

namespace BusinessObject.Model
{
    public partial class TransactionsHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int OrderId { get; set; }
        public decimal? Total { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
