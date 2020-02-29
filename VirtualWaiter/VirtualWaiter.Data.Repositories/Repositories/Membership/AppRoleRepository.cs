using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using VirtualWaiter.Domain;
using VirtualWaiter.EntityFramework;
using VirtualWaiter.Infrastructure;
using VirtualWaiter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Data
{
    public class AppRoleRepository : Repository<AppRole, MainDatabaseContext>, IAppRoleRepository
    {
        public AppRoleRepository(MainDatabaseContext context)
         : base(context)
        {
        }
        public object GetRolesToList(DataSourceLoadOptionsBase loadOptions)
        {
            IQueryable<RolerListDTO> query = _dbset.Select(x => new RolerListDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            });
            LoadResult result = DataSourceLoader.Load(query, loadOptions);
            return result;
        }

        public List<SelectModelBinder<int>> GetRolesToSelect()
        {
            List<SelectModelBinder<int>> result = _dbset.Select(x => new SelectModelBinder<int>()
            {
                Value = x.Id,
                Text = x.Name
            }).OrderBy(x => x.Text).ToList();

            return result;
        }
    }
}
