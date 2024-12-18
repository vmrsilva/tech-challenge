using TechChallange.Domain.Base.Entity;
using TechChallange.Domain.Contact.Entity;

namespace TechChallange.Domain.Region.Entity
{
    public class RegionEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Ddd { get; set; }
        public  IList<ContactEntity> Contacts { get; set; }
    }
}
