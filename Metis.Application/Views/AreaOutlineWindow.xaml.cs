using System;
using System.Windows;
using Metis.Core.Display;

namespace Metis.Application.Views
{
    internal partial class AreaOutlineWindow : IOutlineArea
    {
        bool IDisplayElement.IsVisible
        {
            get => Visibility == Visibility.Visible;
            set => Visibility = value ? Visibility.Visible : Visibility.Hidden;
        }

        public double OuterX
        {
            get => Left;
            set => Left = value;
        }

        public double OuterY
        {
            get => Top;
            set => Top = value;
        }

        public double OuterWidth
        {
            get => Width;
            set => Width = value;
        }

        public double OuterHeight
        {
            get => Height;
            set => Height = value;
        }

        public double ThicknessLeft
        {
            get => Border.BorderThickness.Left;
            set => Border.BorderThickness = new Thickness(value, ThicknessTop, ThicknessRight, ThicknessBottom);
        }

        public double ThicknessTop
        {
            get => Border.BorderThickness.Top;
            set => Border.BorderThickness = new Thickness(ThicknessLeft, value, ThicknessRight, ThicknessBottom);
        }

        public double ThicknessRight
        {
            get => Border.BorderThickness.Right;
            set => Border.BorderThickness = new Thickness(ThicknessLeft, ThicknessTop, value, ThicknessBottom);
        }

        public double ThicknessBottom
        {
            get => Border.BorderThickness.Bottom;
            set => Border.BorderThickness = new Thickness(ThicknessLeft, ThicknessTop, ThicknessRight, value);
        }

        internal AreaOutlineWindow(int x, int y, int width, int height, int thicknessLeft = 3, int thicknessTop = 3, int thicknessRight = 3, int thicknessBottom = 3)
        {
            InitializeComponent();
            Left = x;
            Top = y;
            Width = width;
            Height = height;
            Border.BorderThickness = new Thickness(thicknessLeft, thicknessTop, thicknessRight, thicknessBottom);
        }

        internal AreaOutlineWindow() : this(0, 0, 0, 0) { }

        private void AreaOutlineWindow_OnDeactivated(object? sender, EventArgs e)
        {
            Topmost = true;
            Activate();
        }
    }
}
