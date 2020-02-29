using VirtualWaiter.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.EntityFramework
{
    public class AppUserRoleConfiguration : EntityTypeConfiguration<AppUserRole>
    {
        public AppUserRoleConfiguration()
        {
            HasRequired(x => x.AppRole)
                .WithMany()
                .HasForeignKey(x => x.AppRoleId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.AppUser)
             .WithMany(x => x.AppUserRoles)
             .HasForeignKey(x => x.AppUserId)
             .WillCascadeOnDelete(false);
        }
    }
}
