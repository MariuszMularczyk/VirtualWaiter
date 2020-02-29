using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using VirtualWaiter.Dictionaries;
using VirtualWaiter.Domain;
using VirtualWaiter.EntityFramework;
using VirtualWaiter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Data
{
    public class FunctionalityAppRoleRepository : Repository<FunctionalityAppRole, MainDatabaseContext>, IFunctionalityAppRoleRepository
    {
        public FunctionalityAppRoleRepository(MainDatabaseContext context)
         : base(context)
        {
        }

        public object GetRoleFunctionality(DataSourceLoadOptionsBase loadOptions, int roleId)
        {
            IQueryable<RoleFunctionalityListDTO> query = _dbset.Where(x => x.AppRoleId == roleId).Select(x => new RoleFunctionalityListDTO()
            {
                FunctionalityRoleId = x.Id,
                Description = x.Functionality.Description,
                Name = x.Functionality.Name
            });
            LoadResult result = DataSourceLoader.Load(query, loadOptions);
            return result;
        }
        public object GetFunctionalitiesToAdd(DataSourceLoadOptionsBase loadOptions, int roleId)
        {
            IQueryable<RoleFunctionalityToAddListDTO> query = from functionality in Context.Functionalities
                                                              join functionalityRole in Context.FunctionalityAppRoles.Where(x => x.AppRoleId == roleId) on functionality.Id equals functionalityRole.FunctionalityId into joinedFunctionalityRoles
                                                              from data in joinedFunctionalityRoles.DefaultIfEmpty()
                                                              select new RoleFunctionalityToAddListDTO()
                                                              {
                                                                  Id = functionality.Id,
                                                                  Description = functionality.Description,
                                                                  Name = functionality.Name,
                                                                  IsAdded = data != null
                                                              };

            LoadResult result = DataSourceLoader.Load(query, loadOptions);
            return result;
        }
    }
}
