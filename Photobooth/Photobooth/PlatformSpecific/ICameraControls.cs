using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Photobooth.PlatformSpecific
{
    interface ICameraControls
    {
        Task<Stream> TakePhotoAsync();
    }
}
