namespace Metis.Core.Display
{
    public interface IOutlineArea : IDisplayElement
    {
        public double OuterX { get; set; }
        public double OuterY { get; set; }
        public double OuterWidth { get; set; }
        public double OuterHeight { get; set; }

        public double ThicknessLeft { get; set; }
        public double ThicknessTop { get; set; }
        public double ThicknessRight { get; set; }
        public double ThicknessBottom { get; set; }
    }
}
