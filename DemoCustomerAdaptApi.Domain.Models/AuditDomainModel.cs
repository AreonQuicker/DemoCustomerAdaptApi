using System;

namespace DemoCustomerAdaptApi.Domain.Models
{
    public abstract class AuditDomainModel
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }
    }
}