using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace WindowsFormsUtilities
{
   public class WindowsImageConverter
    {

        public static byte[] ConvertImage(Image image)
        {
            if (image == null)
                throw new ArgumentNullException("The image is not exist");

          ImageConverter imageConverter = new ImageConverter();
         return  (byte[])imageConverter.ConvertTo(image, typeof(byte[]));

        }
    }
}
