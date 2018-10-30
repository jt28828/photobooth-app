using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Photobooth.Services
{
    /// <summary>
    /// Not a great way but it's a week before the wedding so I just want it done.
    /// Singleton used to pass around the stream from the camera and lets the viewmodel know it's there via propertyChanged events
    /// </summary>
    public class Storage : INotifyPropertyChanged
    {
        private static Storage _instance;
        public static Storage Instance { get
            {
                if (_instance == null)
                {
                    _instance = new Storage();
                }
               return _instance;
            }
        }

        private Stream _currentImage;
        public Stream CurrentImage {
            get => _currentImage;
            set
            {
                _currentImage = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Called whenever a property is updated and any classes using it need to know.
        /// Called from property setter and automatically fills out propertyName because of CallerMemberName
        /// </summary>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
