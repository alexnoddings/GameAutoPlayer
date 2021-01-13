using System.Windows;
using Metis.Application.Views;
using Metis.Core.Display;

namespace Metis.Application.Models
{
    internal class DisplayHelper : IDisplayHelper
    {
        public double DisplayWidth => SystemParameters.PrimaryScreenWidth;

        public double DisplayHeight => SystemParameters.PrimaryScreenHeight;

        public double DisplayCenterX => DisplayWidth / 2;

        public double DisplayCenterY => DisplayHeight / 2;

        public IOutlineArea CreateOutlineArea() => new AreaOutlineWindow();

        public ISolidArea CreateSolidArea() => new AreaSolidWindow();
    }
}
