using RC.ADS.WebAPP.Comm;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;



namespace RC.ADS.WebAPP.Models.WeChat
{
    public partial class CustomMessageHandler : MessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>

    {

        public CustomMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0) : base(inputStream, postModel)

        {

        }



        public CustomMessageHandler(RequestMessageBase requestMessage) : base(requestMessage)

        {

        }


        public override IResponseMessageBase OnEventRequest(IRequestMessageEventBase requestMessage)
        {

            switch (requestMessage.Event)
            {
                case Event.ENTER:
                    break;
                case Event.LOCATION:
                    break;
                case Event.subscribe:
                    var requestMessagenew = (RequestMessageEvent_Subscribe)requestMessage;
                    RCLog.Info(this, "EventKey：" + requestMessagenew.EventKey);///推荐人openId
                    RCLog.Info(this, "FromUserName：" + requestMessagenew.FromUserName);///订阅人Id
                    RCLog.Info(this, "ToUserName：" + requestMessagenew.ToUserName);///服务号Id
                    break;
                case Event.unsubscribe:
                    break;
                case Event.CLICK:
                    break;
                case Event.scan:
                    break;
                case Event.VIEW:
                    break;
                case Event.MASSSENDJOBFINISH:
                    break;
                case Event.TEMPLATESENDJOBFINISH:
                    break;
                case Event.scancode_push:
                    break;
                case Event.scancode_waitmsg:
                    break;
                case Event.pic_sysphoto:
                    break;
                case Event.pic_photo_or_album:
                    break;
                case Event.pic_weixin:
                    break;
                case Event.location_select:
                    break;
                case Event.card_pass_check:
                    break;
                case Event.card_not_pass_check:
                    break;
                case Event.user_get_card:
                    break;
                case Event.user_del_card:
                    break;
                case Event.kf_create_session:
                    break;
                case Event.kf_close_session:
                    break;
                case Event.kf_switch_session:
                    break;
                case Event.poi_check_notify:
                    break;
                case Event.WifiConnected:
                    break;
                case Event.user_consume_card:
                    break;
                case Event.user_view_card:
                    break;
                case Event.user_enter_session_from_card:
                    break;
                case Event.merchant_order:
                    break;
                case Event.submit_membercard_user_info:
                    break;
                case Event.ShakearoundUserShake:
                    break;
                case Event.user_gifting_card:
                    break;
                case Event.user_pay_from_pay_cell:
                    break;
                case Event.update_member_card:
                    break;
                case Event.card_sku_remind:
                    break;
                case Event.card_pay_order:
                    break;
                case Event.qualification_verify_success:
                    break;
                case Event.qualification_verify_fail:
                    break;
                case Event.naming_verify_success:
                    break;
                case Event.naming_verify_fail:
                    break;
                case Event.annual_renew:
                    break;
                case Event.verify_expired:
                    break;
                case Event.weapp_audit_success:
                    break;
                case Event.weapp_audit_fail:
                    break;
                default:
                    break;
            }
            return base.OnEventRequest(requestMessage);

        }



        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)

        {

            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();

            responseMessage.Content = "这条消息来自于DefaultResponseMessage";

            return responseMessage;

        }



        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)

        {
            RCLog.Info(this, "进入信息处理");
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();

            //responseMessage.Content = "您的OpenID是：" + responseMessage.FromUserName+".\r\n您发送的文字是："+requestMessage.Content;

            if (requestMessage.Content == "ID")

                responseMessage.Content = "您的OpenID是：" + responseMessage.FromUserName;

            if (requestMessage.Content == "天气")

                responseMessage.Content = "抱歉，还未开通此功能！";

            return responseMessage;

        }

    }

}