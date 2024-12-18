using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallange.Domain.Base.Entity
{
    public class BaseEntity
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; set; } = false;


        public void MarkAsDeleted()
        {
            IsDeleted = true;
        }
    }
}
