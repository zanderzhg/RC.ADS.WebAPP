using Microsoft.AspNetCore.Http;
using qcloudsms_csharp;
using qcloudsms_csharp.httpclient;
using qcloudsms_csharp.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RC.ADS.WebAPP.Comm
{
    public static class SMSHelper
    {
        // 短信应用SDK AppID
        static int appid = 1400117019;
        // 短信应用SDK AppKey
        static string appkey = "55587a059f9065fd937fe1393d961cbd";
       
        /// <summary>
        /// 发送验证码
        /// </summary>
        public static bool SendVerificationCode(string VerificationCode, string phoneNumber)
        {
            // 短信模板ID，需要在短信应用中申请
            int templateId = 170466;
            try
            {
                
                SmsSingleSender ssender = new SmsSingleSender(appid, appkey);
                var result = ssender.sendWithParam("86", phoneNumber,
                    templateId, new[] { VerificationCode }, "", "", "");
                Console.WriteLine(result);
                return true;

            }
            catch (JSONException e)
            {
                Console.WriteLine(e);
            }
            catch (HTTPException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="phoneNumbers"></param>
        public static void SendNotification(string msg, List<string> phoneNumbers) { }
    }
}
