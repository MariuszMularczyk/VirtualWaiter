using VirtualWaiter.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.EntityFramework
{
    public class AppRoleConfiguration : EntityTypeConfiguration<AppRole>
    {
        public AppRoleConfiguration()
        {
            HasMany(x => x.FunctionalityAppRoles)
                .WithRequired(x => x.AppRole)
                .HasForeignKey(x => x.AppRoleId)
                .WillCascadeOnDelete(false);
        }
    }
}
