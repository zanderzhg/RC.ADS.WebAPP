using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace RC.ADS.WebAPP.Controllers
{
    public class UploadController : Controller
    {
        private IHostingEnvironment _env;
        public UploadController(IHostingEnvironment env)
        {
            _env = env;
        }

        public async Task<IActionResult> UploadImage()
        {
            string callback = Request.Query["CKEditorFuncNum"];//要求返回值
            var upload = Request.Form.Files[0];
            if (upload == null)
                return Json(new { uploaded = 0, error = new { message = "请选择一张图片" } });
            //判断是否是图片类型
            List<string> imgtypelist = new List<string> { "image/pjpeg", "image/png", "image/x-png", "image/gif", "image/bmp" };
            if (imgtypelist.FindIndex(x => x == upload.ContentType) == -1)
            {
                return Json(new { uploaded = 0, error = new { message = "请上传一张图片!" } });
            }
            var data = Request.Form.Files["upload"];
            string filepath = _env.WebRootPath + "\\userfile\\images";
            //string imgname = Utils.GetOrderNum() + Utils.GetFileExtName(upload.FileName);
            string imgname = DateTime.Now.Ticks.ToString() + Path.GetExtension(upload.FileName);

            string fullpath = Path.Combine(filepath, imgname);
            try
            {
                if (!Directory.Exists(filepath))
                    Directory.CreateDirectory(filepath);
                if (data != null)
                {
                    await Task.Run(() =>
                    {
                        using (FileStream fs = new FileStream(fullpath, FileMode.Create))
                        {
                            data.CopyTo(fs);
                        }
                    });
                }
            }
            catch (Exception ex)
            { 
                return Json(new { uploaded = 0, error = new { message = "图片上传失败" } });
                
            }
 
            return Json(new { uploaded=1, fileName= imgname, url= "/userfile/images/" + imgname });
        }
    }

}