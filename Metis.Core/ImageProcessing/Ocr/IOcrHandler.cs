using System.Drawing;

namespace Metis.Core.ImageProcessing.Ocr
{
    public interface IOcrHandler
    {
        public string GetText(Bitmap image);
    }
}
