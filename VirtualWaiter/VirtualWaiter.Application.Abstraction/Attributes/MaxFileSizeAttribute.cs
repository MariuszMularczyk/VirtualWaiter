using VirtualWaiter.Resources.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VirtualWaiter.Application
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            int maxContent = 5 * 1024 * 1024; //5 MB
            if (value is HttpPostedFileBase)
            {
                var file = value as HttpPostedFileBase;
                if (file.ContentLength > maxContent)
                {
                    ErrorMessage = string.Format(ErrorResource.FileTooLarge, maxContent / (1024 * 1024));
                    return false;
                }
            }
            else if (value is List<HttpPostedFileBase>)
            {
                var files = value as List<HttpPostedFileBase>;
                foreach (var file in files)
                    if (file.ContentLength > maxContent)
                    {
                        ErrorMessage = string.Format(ErrorResource.OneFileIsTooLarge, maxContent / (1024 * 1024));
                        return false;
                    }
            }
            return true;
        }
    }
}
