using System;
using System.Collections.Generic;

namespace BusinessObject.Model
{
    public partial class UserDissucss
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DisscussId { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Discussion Disscuss { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
