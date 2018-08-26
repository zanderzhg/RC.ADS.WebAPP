$(function () {
    $("#timeCode").hide();
    //点击获取验证码
    $("#getCode").click(function () {
        //获取手机号
        var PhoneNumber = $("#PhoneNumber").val();
        // 手机号正则
        var reg = /^[1][3,4,5,7,8][0-9]{9}$/;
        if (reg.test(PhoneNumber)) {
            console.log("IN");
            $.post(
                '/wechat/SendPhoneValidateCode', //提交到那里
                { "PhoneNumber": PhoneNumber},//提交的数据
                function (data) {
                    console.log(data);
                    if (data.statu == "OK") {
                        $.toast(data.msg);
                        reserveCode();
                    } else {
                        $.toast(data.msg, "forbidden");
                    }
                }
            );
        } else {
            $.toast("手机号格式错误", "forbidden");
        }
    });
   

    function reserveCode() {
        //显示60s倒计时，隐藏‘获取验证码’
        var time = 0;
        //倒计时-打开
        $("#timeCode").show();
        //获取-隐藏
        $("#getCode").hide();
        timer = setInterval(function () {
            time = parseInt($("#timeCode").html());
            time--;
            $("#timeCode").html(time + "秒后获取");
            //60s后停止定时器
            if (time == 0) {
                $("#timeCode").html("60秒后获取").hide();
                $("#getCode").show();
                clearInterval(timer);
            }
        }, 1000)
    }


})