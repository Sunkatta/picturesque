using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Picturesque.Domain
{
    public interface IPictureServiceManager
    {
        Task<IEnumerable<PictureView>> GetPicturesAsync();
    }
}
