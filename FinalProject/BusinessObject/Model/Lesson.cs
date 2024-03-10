using System;
using System.Collections.Generic;

namespace BusinessObject.Model
{
    public partial class Lesson
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? VideoUrl { get; set; }
        public int Order { get; set; }
        public bool? IsPublished { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Course Course { get; set; } = null!;
    }
}
