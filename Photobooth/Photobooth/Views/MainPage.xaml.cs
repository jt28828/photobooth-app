using Photobooth.PlatformSpecific;
using Photobooth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Photobooth.Views
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _viewModel;
        private bool _coverVisible = true;
        private bool _pulseForward = true;

        private ICameraControls _cameraControls;

        public MainPage()
        {
            BindingContext = _viewModel = new MainPageViewModel();
            InitializeComponent();
            _cameraControls = DependencyService.Get<ICameraControls>();
            PulseButton();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (_coverVisible)
            {
                // Hide
                _coverVisible = !_coverVisible;
                ViewExtensions.CancelAnimations(MainButton);
                AnimationContainer.ScaleTo(0, 300, Easing.CubicOut);
                await MainButton.ScaleTo(0, 300, Easing.CubicOut);
                MainButton.InputTransparent = true;
                Countdown();
            } else
            {
                // Show
                _coverVisible = !_coverVisible;
                AnimationContainer.ScaleTo(1, 300, Easing.CubicOut);
                await MainButton.ScaleTo(0, 300, Easing.CubicOut);
                MainButton.InputTransparent = false;
                PulseButton();
            }
        }

        private async void Countdown(int number = 5)
        {
            if (number != 0)
            {
                CountdownNumber.Text = number.ToString();
                await CountdownNumber.ScaleTo(1, 350, Easing.CubicInOut);
                await Task.Delay(300);
                await CountdownNumber.FadeTo(0, 150, Easing.CubicOut);
                CountdownNumber.Scale = 0;
                CountdownNumber.Opacity = 1;
                Countdown(--number);
            }
            else
            {
                // Take photo

                // Show
                _coverVisible = !_coverVisible;
                AnimationContainer.ScaleTo(1, 300, Easing.CubicOut);
                await MainButton.ScaleTo(0, 300, Easing.CubicOut);
                MainButton.InputTransparent = false;
                PulseButton();
            }
        }

        /// <summary>
        /// Pulse the take photo button
        /// </summary>
        private async void PulseButton()
        {
            if (_coverVisible)
            {
                // Is visible
                if (_pulseForward)
                {
                    await MainButton.ScaleTo(0.9, 600, Easing.CubicInOut);
                } else
                {
                    await MainButton.ScaleTo(1.0, 600, Easing.CubicInOut);
                }
                _pulseForward = !_pulseForward;
                PulseButton();
            }
        }
    }
}
