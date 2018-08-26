using Microsoft.AspNetCore.Http;
using qcloudsms_csharp;
using qcloudsms_csharp.httpclient;
using qcloudsms_csharp.json;
using RC.ADS.Data;
using RC.ADS.Data.Entity.AD_SMS;
using RC.ADS.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RC.ADS.WebAPP.Comm
{
    public  class SMSHelper
    {
        private readonly DataContext _context;
        public SMSHelper(DataContext context) {
            _context = context;
        }     
      
        public SMSAppTemplate GetSMSAppTemplate(SMSTemplateType smsType)
        {
            DateTime dt = DateTime.Now;
            //统计X年X月，App发送了多少条短信，取发送少于100条的数据
            var appSendQtys = _context.SendSMSLogs.Where(x => x.Year == DateTime.Now.Year && x.Month == DateTime.Now.Month)
                .OrderByDescending(x => x.CreateTime)
                .GroupBy(x=>x.Appid)
                .Select(x=>new {
                    appid = x.Key,
                    sendSmsQty=x.FirstOrDefault()==null?0:x.FirstOrDefault().QuantityAfterSend
                });
    
            var result = (from T in _context.SMSAppTemplates
                          join A in _context.SMSApp on T.Appid equals A.appid
                          where T.TemplateType== smsType
                          select T).FirstOrDefault();
                        
            return result;
        }
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="context"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public bool SendVerificationCode2(string[] context, string phoneNumber)
        {
            var template = GetSMSAppTemplate(SMSTemplateType.verification);
            try
            {
                SmsSingleSender ssender = new SmsSingleSender(Convert.ToInt32(template.Appid), template.Appkey);
                var result = ssender.sendWithParam("86", phoneNumber, Convert.ToInt32(template.TemplateId), context, "", "", "");

                Console.WriteLine(result);
                var lastSendSMSLogs = _context.SendSMSLogs.Where(x => x.Year == DateTime.Now.Year && x.Month == DateTime.Now.Month).OrderByDescending(x => x.CreateTime).FirstOrDefault();
                SendSMSLog log = new SendSMSLog() {
                    Appid = template.Appid,
                    SMSAppTemplateId = template.TemplateId,
                    Month = DateTime.Now.Month,
                    Year = DateTime.Now.Year,
                    Content = string.Join(",", context),
                    phoneNumbers = phoneNumber,
                    QuantityBeforeSend = lastSendSMSLogs == null ? 0 : lastSendSMSLogs.QuantityAfterSend,
                    QuantityAfterSend = lastSendSMSLogs == null ? 1 : lastSendSMSLogs.QuantityAfterSend+1,
                    CreateTime =DateTime.Now
                };
                _context.Add(log);
                _context.SaveChanges();
                return true;

            }
            catch (JSONException e)
            {
                RCLog.Error(this, e.ToString());

            }
            catch (HTTPException e)
            {
                RCLog.Error(this, e.ToString());

            }
            catch (Exception e)
            {
                RCLog.Error(this, e.ToString());

            }
            return false;
        }

        // 短信应用SDK AppID
        static int appid = 1400117019;
        // 短信应用SDK AppKey
        static string appkey = "55587a059f9065fd937fe1393d961cbd";
        /// <summary>
        /// 发送验证码
        /// </summary>
        public  bool SendVerificationCode(string VerificationCode, string phoneNumber)
        {
            // 短信模板ID，需要在短信应用中申请
            int templateId = 170466;
            try
            {
                SmsSingleSender ssender = new SmsSingleSender(appid, appkey);
                var result = ssender.sendWithParam("86", phoneNumber,
                    templateId, new[] { VerificationCode }, "", "", "");
                if (result.result==0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
               

            }
            catch (JSONException e)
            {
                RCLog.Error(this,e.ToString());
            }
            catch (HTTPException e)
            {
                RCLog.Error(this, e.ToString());

            }
            catch (Exception e)
            {
                RCLog.Error(this, e.ToString());

            }
            return false;
        }
        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="phoneNumbers"></param>
        public  void SendNotification(string msg, List<string> phoneNumbers) { }
    }
}
