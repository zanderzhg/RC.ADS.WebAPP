using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;

namespace RC.ADS.WebAPP.Comm
{
    public class BarCodeHelper
    {
        // 生成二维码
        public static MemoryStream CreateCodeEwm(string message, int width = 600, int height = 600)

        {
            MemoryStream ms = null;
            int heig = width;
            if (width > height)
            {
                heig = height;
                width = height;
            }
            if (string.IsNullOrWhiteSpace(message))
            {
                return ms;
            }
            var w = new ZXing.QrCode.QRCodeWriter();
            BitMatrix b = w.encode(message, BarcodeFormat.QR_CODE, width, heig);
            var zzb = new ZXing.ZKWeb.BarcodeWriter();
            zzb.Options = new EncodingOptions()
            {
                Margin = 0,
            };
            Bitmap b2 = zzb.Write(b);
            ms = new MemoryStream();//生成内存流对象  
            b2.Save(ms, ImageFormat.Jpeg);//将此图像以Png图像文件的格式保存到流中 

            //b2.Save(gifFileName, ImageFormat.Gif);
            b2.Dispose();

            return ms;

        }

        /// <summary>

        /// 读取二维码或者条形码从图片

        /// </summary>

        /// <param name="imgFile"></param>

        /// <returns></returns>

        public static string ReadFromImage(string imgFile)

        {



            if (string.IsNullOrWhiteSpace(imgFile))

            {

                return "";

            }

            Image img = Image.FromFile(imgFile);

            Bitmap b = new Bitmap(img);



            //该类名称为BarcodeReader,可以读二维码和条形码

            var zzb = new ZXing.ZKWeb.BarcodeReader();

            zzb.Options = new DecodingOptions

            {

                CharacterSet = "UTF-8"

            };

            Result r = zzb.Decode(b);

            string resultText = r.Text;

            b.Dispose();

            img.Dispose();



            return resultText;



        }



        //将Bitmap  写为byte[]的方法

        public static byte[] BitmapToArray(Bitmap bmp)

        {

            byte[] byteArray = null;



            using (MemoryStream stream = new MemoryStream())

            {



                bmp.Save(stream, ImageFormat.Png);

                byteArray = stream.GetBuffer();

            }



            return byteArray;

        }

        // 生成条形码

        public static void CreateCodeTxm(string message, string gifFileName, int width, int height)

        {



            if (string.IsNullOrWhiteSpace(message))
            {

                return;

            }



            var w = new ZXing.OneD.CodaBarWriter();

            BitMatrix b = w.encode(message, BarcodeFormat.ITF, width, height);

            var zzb = new ZXing.ZKWeb.BarcodeWriter();

            zzb.Options = new EncodingOptions()

            {

                Margin = 3,

                PureBarcode = true

            };

            string dir = Path.GetDirectoryName(gifFileName);

            if (!Directory.Exists(dir))

            {

                Directory.CreateDirectory(dir);

            }

            Bitmap b2 = zzb.Write(b);

            b2.Save(gifFileName, ImageFormat.Gif);

            b2.Dispose();

        }







    }

}
