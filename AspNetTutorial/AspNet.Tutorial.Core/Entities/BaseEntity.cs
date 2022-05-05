using System;

namespace AspNet.Tutorial.Core.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public Guid? CreatedBy { get; set; }

        public Guid? ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}
