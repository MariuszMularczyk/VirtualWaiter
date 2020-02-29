using VirtualWaiter.Data;
using VirtualWaiter.Dictionaries;
using VirtualWaiter.Domain;
using VirtualWaiter.Infrastructure;
using VirtualWaiter.Resources.Shared;
using VirtualWaiter.Utils;
using DevExtreme.AspNet.Data;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Application
{
    public class FunctionalityAppRoleService : ServiceBase, IFunctionalityAppRoleService
    {
        #region Dependencies    
        [Inject]
        public IAppRoleRepository AppRoleRepository { get; set; }
        [Inject]
        public IFunctionalityAppRoleRepository FunctionalityAppRoleRepository { get; set; }
        [Inject]
        public FunctionalityAppRoleConverter FunctionalityAppRoleConverter { get; set; }
        #endregion


        public void Delete(int functionalityRoleId)
        {
            FunctionalityAppRoleRepository.DeleteWhere(x => x.Id == functionalityRoleId);
        }

        public object GetRoleFunctionalities(DataSourceLoadOptionsBase loadOptions, int roleId)
        {
            return FunctionalityAppRoleRepository.GetRoleFunctionality(loadOptions, roleId);
        }

        public FunctionalityAppRoleAddVM GetFunctionalityAppRoleAddVM(int roleId)
        {
            AppRole role = AppRoleRepository.GetSingle(x => x.Id == roleId);
            if (role.AppRoleType == AppRoleType.Administrator)
                throw new BussinesException(3, ErrorResource.CanNotAddFunctionalityToAdminRole);
            FunctionalityAppRoleAddVM result = FunctionalityAppRoleConverter.ToFunctionalityAppRoleAddVM(role);
            return result;
        }

        public void Add(int functionalityId, int roleId)
        {
            AppRole role = AppRoleRepository.GetSingle(x => x.Id == roleId);
            if (role.AppRoleType == AppRoleType.Administrator)
                throw new BussinesException(3, ErrorResource.CanNotAddFunctionalityToAdminRole);
            if (IsFunctionalityRoleAdded(functionalityId, roleId))
                return;
            var entity = new FunctionalityAppRole()
            {
                FunctionalityId = functionalityId,
                AppRoleId = roleId
            };
            FunctionalityAppRoleRepository.Add(entity);
        }

        public bool IsFunctionalityRoleAdded(int functionalityId, int roleId)
        {
            return FunctionalityAppRoleRepository.Any(x => x.AppRoleId == roleId && x.FunctionalityId == functionalityId);
        }

        public object GetFunctionalitiesToAdd(DataSourceLoadOptionsBase loadOptions, int roleId)
        {
            return FunctionalityAppRoleRepository.GetFunctionalitiesToAdd(loadOptions, roleId);
        }
    }
}
