using System;
using System.Windows;
using Metis.Core.Display;

namespace Metis.Application.Views
{
    public partial class AreaSolidWindow : ISolidArea
    {
        bool IDisplayElement.IsVisible
        {
            get => Visibility == Visibility.Visible;
            set => Visibility = value ? Visibility.Visible : Visibility.Hidden;
        }

        public double X
        {
            get => Left;
            set => Left = value;
        }

        public double Y
        {
            get => Top;
            set => Top = value;
        }

        internal AreaSolidWindow(int x, int y, int width, int height)
        {
            InitializeComponent();
            Left = x;
            Top = y;
            Width = width;
            Height = height;
        }

        internal AreaSolidWindow() : this(0, 0, 0, 0) { }

        private void AreaSolidWindow_OnDeactivated(object? sender, EventArgs e)
        {
            Topmost = true;
            Activate();
        }
    }
}
