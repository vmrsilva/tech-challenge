using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallange.Domain.Contact.Entity;
using TechChallange.Domain.Region.Entity;

namespace TechChallange.Infrastructure.Context
{
    public class TechChallangeContext : DbContext
    {
        public TechChallangeContext() : base()
        {

        }

        public TechChallangeContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<RegionEntity> Region { get; set; }
        public DbSet<ContactEntity> Contact { get; set; }
    }
}
