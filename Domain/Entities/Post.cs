using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Post : BaseEntity
    {
        public required string Title { get; set; }
        public string? Content { get; set; }
        public Guid? CategoryId { get; set; }

    }
}
