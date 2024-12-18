using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallange.Domain.Base.Entity;
using TechChallange.Domain.Region.Entity;

namespace TechChallange.Domain.Contact.Entity
{
    public class ContactEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid RegionId { get; set; }
        public virtual RegionEntity Region { get; set; }

    }
}
