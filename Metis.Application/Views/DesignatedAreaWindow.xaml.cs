using System;
using System.Windows;
using System.Windows.Input;

namespace Metis.Application.Views
{
    internal partial class DesignatedAreaWindow
    {
        private bool _isTracking;
        private Point _downPoint = new Point(0, 0);
        private double _startWidth;
        private double _startHeight;

        public DesignatedAreaWindow(int x, int y, int width, int height)
        {
            InitializeComponent();
        }

        private void DesignatedAreaWindow_OnDeactivated(object? sender, EventArgs e)
        {
            Topmost = true;
            Activate();
        }
        
        private void MoveIcon_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void SizeIcon_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isTracking = true;
            _startWidth = Width;
            _startHeight = Height;
            _downPoint = Mouse.GetPosition(this);
        }

        private void SizeIcon_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isTracking = false;
        }

        private void SizeIcon_OnMouseLeave(object sender, MouseEventArgs e)
        {
            _isTracking = false;
        }

        private void SizeIcon_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_isTracking) return;
            Point m = Mouse.GetPosition(this);
            double dx = m.X - _downPoint.X;
            double dy = m.Y - _downPoint.Y;
            Width = _startWidth + dx;
            Height = _startHeight + dy;
        }
    }
}
