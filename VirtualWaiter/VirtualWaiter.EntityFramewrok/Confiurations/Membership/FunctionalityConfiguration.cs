using VirtualWaiter.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.EntityFramework
{
    public class FunctionalityConfiguration : EntityTypeConfiguration<Functionality>
    {
        public FunctionalityConfiguration()
        {
            HasMany(x => x.FunctionalityAppRoles)
            .WithRequired(x => x.Functionality)
            .HasForeignKey(x => x.FunctionalityId)
            .WillCascadeOnDelete(false);

        }
    }
}
