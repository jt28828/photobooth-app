using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PhotoBooth.PlatformSpecific
{
    interface ICameraControls
    {
        Task<Stream> TakePhotoAsync();
    }
}
