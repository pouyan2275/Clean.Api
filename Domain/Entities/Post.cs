using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Bases.Interfaces.Entities;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Post : IBaseEntity<Guid>
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid Id { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
