using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallange.Domain.Base.Entity;
using TechChallange.Domain.Region.Entity;

namespace TechChallange.Domain.Contact.Entity
{
    public class ContactEntity : BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(9)]
        public string Phone { get; set; }
        [MaxLength(80)]
        public string Email { get; set; }
        public Guid RegionId { get; set; }
        public virtual RegionEntity Region { get; set; }

    }
}
