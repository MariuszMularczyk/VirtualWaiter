using VirtualWaiter.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.EntityFramework
{
    public class FunctionalityAppRoleConfiguration : EntityTypeConfiguration<FunctionalityAppRole>
    {
        public FunctionalityAppRoleConfiguration()
        {
            HasRequired(x => x.AppRole)
            .WithMany(x => x.FunctionalityAppRoles)
            .HasForeignKey(x => x.AppRoleId)
            .WillCascadeOnDelete(false);

            HasRequired(x => x.Functionality)
              .WithMany(x => x.FunctionalityAppRoles)
              .HasForeignKey(x => x.FunctionalityId)
              .WillCascadeOnDelete(false);
        }
    }
}
