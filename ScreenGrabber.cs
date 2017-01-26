using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace ScreenCaster
{
    public class ScreenGrabber
    {
        private readonly Timer timer;

        private byte[] currentImageData;

        public ScreenGrabber()
        {
            timer = new Timer(_ => TimerElapsed(), null, 0, 1000);
            timer.Change(0,0);
        }

        public void Start()
        {
            timer.Change(0,150);
        }

        public byte[] WaitForNextAvailableImage()
        {
            byte[] imageData = currentImageData;
            while (imageData == null)
            {
                imageData = currentImageData;
                System.Threading.Thread.Sleep(50);
            }
            return imageData;
        }

        private void TimerElapsed()
        {
            Interlocked.Exchange(ref currentImageData, CaptureScreenshotAsBytes());
        }

        private byte[] CaptureScreenshotAsBytes()
        {
            
            //TODO find a way to get primary screen size with dotnet core
            var offScreenImage = new Bitmap(1280,
                                            1024,
                                            PixelFormat.Format32bppArgb);

            Graphics.FromImage(offScreenImage)
                    .CopyFromScreen(0, 0, 0, 0, offScreenImage.Size, CopyPixelOperation.SourceCopy);

            using (MemoryStream imageData = new MemoryStream())
            {
                offScreenImage.Save(imageData, ImageFormat.Jpeg);
                return imageData.ToArray();
            }
        }
    }
}