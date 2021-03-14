﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginOTP.aspx.cs" Inherits="Login_demo.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css"/>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>


    <style type="text/css">
        @import url(https://fonts.googleapis.com/css?family=Exo:400,500,500italic,400italic,600,600italic,700,700italic,800,800italic,300,300italic);

        body {
  padding-top: 10px;
	/*The below background is just to add some color, you can set this to anything*/
  background: url(https://www.magic4walls.com/wp-content/uploads/2014/01/texture-blue-fonchik-simple-dark-colors-glow-background.jpg) no-repeat;
}

.login-form{width:390px;}
.login-title{font-family: 'Exo', sans-serif;text-align:center;color: white;}
.login-userinput{margin-bottom: 10px;}
.login-button{margin-top:10px;}
.login-options{margin-bottom:0px;}
.login-forgot{float: right;}
    </style>
<script type="text/javascript">
    window.onload = function () { $("#showPassword").hide(); }

    $("#txtPassword").on('change', function () {
        if ($("#txtPassword").val()) {
            $("#showPassword").show();
        }
        else {
            $("#showPassword").hide();
        }
    });

    $(".reveal").on('click', function () {
        var $pwd = $("#txtPassword");
        if ($pwd.attr('type') === 'password') {
            $pwd.attr('type', 'text');
        }
        else {
            $pwd.attr('type', 'password');
        }
    });
</script>

    <title>OTP Login Page</title>

    </head>
<body>
   <form runat="server">
       <asp:ScriptManager ID="LoginPnl" runat="server"></asp:ScriptManager>
    <div class="container login-form">
	<h2 class="login-title">- Please Login -</h2>
	<div class="panel panel-default">

		<div class="panel-body">
		
				<div class="input-group login-userinput">
					<span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                    <asp:TextBox ID="txtUser" class="form-control" runat="server" placeholder="Username" AutoComplete="off"></asp:TextBox>
				</div>


				<div class="input-group login-userinput"" >
					<span class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></span>
                     <%--<asp:TextBox ID="txtPassword" runat="server" class="form-control" name="password" placeholder="Password"></asp:TextBox>--%>
                    <asp:TextBox ID="txtPassword" runat="server" class="form-control" name="password" placeholder="Password" AutoComplete="off"></asp:TextBox>
					<span id="showPassword" class="input-group-btn">
                        <button class="btn btn-default reveal" type="button"><i class="glyphicon glyphicon-eye-open"></i></button>
                     </span>  
				</div>		

            
            	<div class="input-group login-userinput"">
					<span class="input-group-addon"><span class="glyphicon glyphicon-phone"></span></span>
                    <asp:TextBox ID="txtPhone" class="form-control" runat="server" placeholder="Mobile No" AutoComplete="off"></asp:TextBox>
                   <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Format" ValidationExpression="^[0-9]{10}$" ControlToValidate="txtPhone"></asp:RegularExpressionValidator>--%>
				</div>
               	
                            <asp:Button ID="Btnsubmit" class="btn btn-primary btn-block login-button" runat="server" Text="Login" OnClick="Btnsubmit_Click" />
				<div class="checkbox login-options">
					<label><input type="checkbox"/> Remember Me</label>
					<a href="#" class="login-forgot">Forgot Username/Password</a>
				</div>		
				
              <div class="modal-body">
                        <asp:TextBox ID="txtOTP" CssClass="form-control" placeholder="One Time Password" runat="server" AutoComplete="off"></asp:TextBox>
                    </div>
                    <div class="modal-footer">                     
                        <asp:Button ID="verifyOTP" class="btn btn-success" UseSubmitBehavior="false" data-dismiss="modal" runat="server" Text="Validate OTP" OnClick="ValidateOTP_Click"/>
                        <asp:HyperLink ID="HyperLink1" runat="server">If you have not receive your OTP then click here</asp:HyperLink>
                    </div>
            <div class="modal-footer">
		        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <asp:HyperLink ID="hylnNext" runat="server" NavigateUrl="~/SignUpOTP.aspx">Go To Sign Up Page</asp:HyperLink>
				</div>
		</div>
	</div>
</div>
        <div class="modal fade" id="send_OTP" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header text-center">
                        <a class="btn pull-right" data-dismiss="modal"><span>&times;</span></a>
                        <h3 class="register_header"><strong>Please enter phone number to receive the code Or Enter different the number below :</strong></h3>
                    </div>
                    <div class="modal-body">
                        <asp:TextBox ID="txtPhoneNo" CssClass="form-control"  runat="server"></asp:TextBox>
                    </div>
                    <div class="modal-footer">                     
                        <asp:Button ID="btnSendOTP" class="btn btn-success" UseSubmitBehavior="false" data-dismiss="modal" runat="server" Text="Send OTP" OnClick="btnSendOTP_Click" />                      
                    </div>
                    <asp:Label ID="lblSend" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>

       <div class="modal fade" id="Receive_OTP" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header text-center">
                        <a class="btn pull-right" data-dismiss="modal"><span>&times;</span></a>
                        <h3 class="register_header"><strong>Please enter the OTP(One Time Password) sent to your registered mobile</strong></h3>
                        <h4>This OTP will expire in 5 minutes.</h4>

                    </div>
                  <%--  <div class="modal-body">
                        <asp:TextBox ID="txtOTP" CssClass="form-control" placeholder="One Time Password" runat="server"></asp:TextBox>
                    </div>
                    <div class="modal-footer">                     
                        <asp:Button ID="verifyOTP" class="btn btn-success" UseSubmitBehavior="false" data-dismiss="modal" runat="server" Text="Validate OTP" OnClick="ValidateOTP_Click"/>
                        <asp:HyperLink ID="HyperLink1" runat="server">If you have not receive your OTP then click here</asp:HyperLink>
                    </div>--%>
                </div>
            </div>
        </div>
             
</form>
</body>
</html>