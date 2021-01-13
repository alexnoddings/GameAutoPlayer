using System;
using System.Windows;
using Metis.Core.Display;

namespace Metis.Application.Views
{
    internal partial class ReticuleWindow : IReticule
    {
        bool IDisplayElement.IsVisible
        {
            get => Visibility == Visibility.Visible;
            set
            {
                if (value)
                    Show();
                else
                    Hide();
            }
        }

        double IReticule.Width
        {
            get => Reticule.Width;
            set => Reticule.Width = value;
        }

        double IReticule.Height
        {
            get => Reticule.Height;
            set => Reticule.Height = value;
        }

        public ReticuleWindow(int width, int height)
        {
            InitializeComponent();
            Reticule.Width = width;
            Reticule.Height = height;
            Left = (SystemParameters.PrimaryScreenWidth / 2) - (Width / 2);
            Top = (SystemParameters.PrimaryScreenHeight / 2) - (Height / 2);
        }

        public ReticuleWindow() : this(2, 2)
        { }

        private void ReticuleWindow_OnDeactivated(object? sender, EventArgs e)
        {
            Topmost = true;
            Activate();
        }
    }
}
