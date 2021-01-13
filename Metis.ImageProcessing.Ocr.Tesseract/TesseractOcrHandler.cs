using System;
using System.Drawing;
using System.IO;
using Metis.Core.ImageProcessing.Ocr;
using Tesseract;
using ImageFormat = System.Drawing.Imaging.ImageFormat;

namespace Metis.ImageProcessing.Ocr.Tesseract
{
    public class TesseractOcrHandler : IOcrHandler, IDisposable
    {
        private TesseractEngine _engine;

        public TesseractOcrHandler()
        {
            _engine = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
            _engine.SetVariable("tessedit_write_images", true);
        }

        /// <inheritdoc />
        public string GetText(Bitmap image)
        {
            using var byteStream = new MemoryStream();
            image.Save(byteStream, ImageFormat.Tiff);
            using Pix img = Pix.LoadTiffFromMemory(byteStream.ToArray());
            using Page page = _engine.Process(img);
            return page.GetText();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _engine.Dispose();
        }
    }
}
