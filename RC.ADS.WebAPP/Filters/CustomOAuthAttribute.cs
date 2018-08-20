using System.Web;
using Microsoft.AspNetCore.Http;
using RC.ADS.WebAPP.Comm;
using Senparc.Weixin.MP.CoreMvcExtension;

namespace RC.ADS.WebAPP.Filters
{
    /// <summary>
    /// OAuth自动验证，可以加在Action或整个Controller上
    /// </summary>
    public class CustomOAuthAttribute : SenparcOAuthAttribute
    {
        public CustomOAuthAttribute(string appId, string oauthCallbackUrl)
            : base(appId, oauthCallbackUrl)
        {
            base._appId = base._appId ?? WeiXinConfig.appId;
        }

        public override bool IsLogined(HttpContext httpContext)
        {
            return httpContext != null && httpContext.Session.GetString("OpenId") != null;

            //也可以使用其他方法如Session验证用户登录
            //return httpContext != null && httpContext.User.Identity.IsAuthenticated;
        }
    }
}