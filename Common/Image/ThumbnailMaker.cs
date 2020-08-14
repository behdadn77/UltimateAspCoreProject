using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Common.Image
{
    class ThumbnailMaker
    {
        public static byte[] CreateThumbnail(System.Drawing.Image image, int width = 1280, int height = 600)
        {
            Bitmap bitmap = new Bitmap(image, width, height);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Jpeg);
                return memoryStream.ToArray();
            }
        }

        public static Stream CreateThumbnailFromStream(Stream stream, int width = 1280, int height = 600,string path="")
        {
            Bitmap bitmap = new Bitmap(System.Drawing.Image.FromStream(stream), width, height);
            using (Stream outStream = new FileStream(path,FileMode.Create))
            {
                bitmap.Save(outStream, ImageFormat.Jpeg);
                return outStream;
            }
        }
    }
}
