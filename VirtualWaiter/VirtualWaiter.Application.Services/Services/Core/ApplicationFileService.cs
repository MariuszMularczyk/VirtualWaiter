using VirtualWaiter.Data;
using VirtualWaiter.Domain;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualWaiter.Resources.Shared;

namespace VirtualWaiter.Application
{
    public class ApplicationFileService : ServiceBase, IApplicationFileService
    {
        #region Dependencies
        [Inject]
        public IApplicationFileRepository ApplicationFileRepository { get; set; }
        #endregion

        public ImageVM GetImage(int id)
        {
            ApplicationFile file = new ApplicationFile();
            file = ApplicationFileRepository.GetImage(id);
            if (file == null)
                throw new BussinesException(502, ErrorResource.NoData);
            ImageVM result = new ImageVM
            {
                Content = file.Content,
                Type = file.ContentType
            };
            return result;
        }
    }
}
