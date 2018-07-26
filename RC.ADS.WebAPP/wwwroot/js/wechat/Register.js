$(function () {
    var url = $("#yzm").attr('src');
    // 添加点击事件 鼠标浮动时变成小手
    $('#yzm').css('cursor', 'pointer').click(function () {
        // 获取到图片的src路径  换一个新的路径   此代码相当与在原来的基础上增加数据
        var code = "?time=" + Date.parse(new Date());
        $('#yzm').attr('src', url + code)
    })

    $("#timeCode").hide();
    //点击获取验证码
    $("#getCode").click(function () {
        //获取手机号
        var mobile = $("#mobile").val();
        var Vcode = $("#Vcode").val();
        // 手机号正则
        var reg = /^[1][3,4,5,7,8][0-9]{9}$/;
        var issuccess = 1;
        if (reg.test(mobile)) {
            $.ajax({
                url: "/verify_mobileCode/", //提交到那里
                type: "get", //提交类型
                data: { "mobile": mobile, "Vcode": Vcode },//提交的数据
                success: function (data) {
                    //success不会直接运行，当服务器有数据传输过来才会触发执行。匿名函数必须要有一个参数，用来接受数据，data1用来接受是服务器端返回字符串数据
                    issuccess = data.statucode;
                    if (data.statucode == 1) {
                        $.toast(data.detail, "forbidden");
                    }
                }
            });
            if (issuccess == 0) {
                //调用倒计时函数
                reserveCode();
            }

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