<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EMS201724112120.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>用户登录</title>
    <link href="lib/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <script src="lib/bootstrap/jquery.min.js"></script>
    <script src="lib/bootstrap/bootstrap.min.js"></script>
    <style>
        .login {
            color: white;
            height: 38px;
            width: 300px;
            background-color: #2b669a;
        }
        #flag{
            margin:60px auto;
        }
    </style>
</head>
<body>
    <div class="container" id="flag">
    <h2 class="text-center">请登录</h2>
    <div class="row">
        <form class="form-horizontal col-md-offset-4 col-md-4" onsubmit="return func()">
            <div class="form-group">
                <label for="inputEmail3" class="col-sm-2 control-label" >账号</label>
                <div class="col-sm-10">
                    <input type="text" class="form-control" id="username" placeholder="请输入账号">
                </div>
            </div>
            <div class="form-group">
                <label for="inputPassword3" class="col-sm-2 control-label" >密码</label>
                <div class="col-sm-10">
                    <input type="password" class="form-control" id="password" placeholder="请输入密码">
                    <span id="message" ></span>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-2 col-sm-10">
                    <button type="submit" class="btn btn-primary  " style="width:290px" onclick="login()">登录</button>
                </div>
            </div>
        </form>
    </div>
</div>
</body>
</html>
<script type="text/javascript">
   
    function func() {
        return false;
    }
    function login() {
        var username = $("#username").val();
        var password = $("#password").val();
        var message = $("#message");
        console.log(username);
        console.log(password);
        console.log("点击了登录");
        if (username == "") {
            message.text("用户名不能为空");
        } else if (password == "") {
            message.text("密码不能为空");
        } else {
            message.text("正在验证，请稍后...");
            $.ajax({
                url: "/Login/Index",
                type: "POST",
                dataType: "json",
                data: {
                    id: username,
                    pwd: password,
                },
                success: function (data) {
                    console.log(data);
                    message.text(data.message);
                    //管理员登录
                    if (data.code == 0) {
                        window.location.href = data.url;
                    }
                    //非管理员登录
                    if (data.code == 1) {
                        window.location.href = data.url;
                    } else {
                        message.text(data.message);
                    }
                },
                error: function (data) {
                    console.log(data.message);
                }
            });
        }
    }
</script>
