using VirtualWaiter.Dictionaries;
using VirtualWaiter.Resources.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VirtualWaiter.Utils
{
    public class FunctionalityAuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        private FunctionalityType[] _funtionalityTypes { get; set; }

        public FunctionalityAuthorizationAttribute(params FunctionalityType[] functionalityTypes)
        {
            _funtionalityTypes = functionalityTypes;
        }
        void IAuthorizationFilter.OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
                // throw new FunctionalityAuthorizationException(String.Format(ErrorResource.AccessDenied));
            }
            {

            }
            IList<FunctionalityType> userFunctionalities = UserHelper.GetUserFunctionalities();

            if (!_funtionalityTypes.Where(x => userFunctionalities.Contains(x)).Any())
            {
                string message = String.Format(ErrorResource.AccessDenied, string.Join(", ", _funtionalityTypes.Select(x => x.GetDisplayName())));

                throw new FunctionalityAuthorizationException(message);

            }
        }
    }
}