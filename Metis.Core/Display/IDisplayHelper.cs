namespace Metis.Core.Display
{
    public interface IDisplayHelper
    {
        public double DisplayWidth { get; }
        public double DisplayHeight { get; }
        public double DisplayCenterX { get; }
        public double DisplayCenterY { get; }

        //public bool IsProcessVisible { get; }

        public IOutlineArea CreateOutlineArea();
    }
}
