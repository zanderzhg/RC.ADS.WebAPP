using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RC.ADS.WebAPP.Comm
{
    public class FileHelper
    {
        public static string UploadImage(IFormFile file, IHostingEnvironment _env)
        {
            if (file == null)
                return "";
            //判断是否是图片类型
            List<string> imgtypelist = new List<string> { "image/pjpeg", "image/png", "image/x-png", "image/gif", "image/bmp" };
            if (imgtypelist.FindIndex(x => x == file.ContentType) == -1)
            {
                //return "请上传一张图片!";
                return "";
            }
            string filepath = _env.WebRootPath + "\\userfile\\images";
            string imgname = DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);

            string fullpath = Path.Combine(filepath, imgname);
            try
            {
                if (!Directory.Exists(filepath))
                    Directory.CreateDirectory(filepath);
                if (file != null)
                {
                    using (FileStream fs = new FileStream(fullpath, FileMode.Create))
                    {
                        //var hash = System.Security.Cryptography.HashAlgorithm.Create();
                        //byte[] hashByte_1 = hash.ComputeHash(fs);
                        file.CopyTo(fs);
                    }

                }
            }
            catch (Exception ex)
            {
                RCLog.Error(nameof(FileHelper), "上传图片失败");
                return "";

            }

            return "/userfile/images/" + imgname;
        }
        //public static string FielHashName(IFormFile file) {
        //    //计算第一个文件的哈希值
        //    var hash = System.Security.Cryptography.HashAlgorithm.Create();
        //    var stream_1 = new System.IO.FileStream(file, System.IO.FileMode.Open);
        //    byte[] hashByte_1 = hash.ComputeHash(stream_1);
        //    stream_1.Close();
        //}
    }
}
