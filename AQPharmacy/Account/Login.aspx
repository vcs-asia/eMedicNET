<%@ Page Language="C#" AutoEventWireup="true" Inherits="Account_Login" Codebehind="Login.aspx.cs" %>
<!DOCTYPE html>
<html lang="en">
	<head>
		<meta charset="utf-8">
		<title>Login - AQ Pharmacy Application</title>
		<meta name="viewport" content="width=device-width, initial-scale=1.0">
		<meta name="description" content="eMedic.NET, Healthcare Solution, Clinic Solution, Hospital Solution">
		<meta name="author" content="Value Creating Solutions, Vijay">

		<!-- Le HTML5 shim, for IE6-8 support of HTML elements -->
		<!--[if lt IE 9]>
		<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
		<![endif]-->

		<!-- Le styles -->
		<link href="<%:ResolveUrl("~/Content/css/bootstrap.css")%>" rel="stylesheet">
		<link href="<%:ResolveUrl("~/Content/css/bootstrap-responsive.css")%>" rel="stylesheet">
		<link href="<%:ResolveUrl("~/Content/css/datepicker.css")%>" rel="stylesheet">
        <link rel="stylesheet" href="<%:ResolveUrl("~/Content/css/font-awesome.min.css")%>">

		<!-- Le fav and touch icons -->
        <!--
		<link rel="shortcut icon" href="<%:ResolveUrl("~/Content/img/abc.ico")%>">
        -->
		<!--
		<link rel="apple-touch-icon-precomposed" sizes="144x144" href="ico/apple-touch-icon-144-precomposed.png">
		<link rel="apple-touch-icon-precomposed" sizes="114x114" href="ico/apple-touch-icon-114-precomposed.png">
		<link rel="apple-touch-icon-precomposed" sizes="72x72" href="ico/apple-touch-icon-72-precomposed.png">
		<link rel="apple-touch-icon-precomposed" href="ico/apple-touch-icon-57-precomposed.png">	
		-->
		<script src="<%:ResolveUrl("~/Content/js/jquery.js")%>"></script>
		<script src="<%:ResolveUrl("~/Content/js/jquery.min.js")%>"></script>
		<script src="<%:ResolveUrl("~/Content/js/bootstrap.js")%>"></script>
		<script src="<%:ResolveUrl("~/Content/js/bootstrap-popover.js")%>"></script>
		<script src="<%:ResolveUrl("~/Content/js/bootstrap-affix.js")%>"></script>
		<script src="<%:ResolveUrl("~/Content/js/bootstrap-fonts.js")%>"></script>
		<script src="<%:ResolveUrl("~/Content/js/application.js")%>"></script>

        <script type="text/javascript">
            function datetime(id) {
                $.ajax({
                    type: 'POST',
                    url: 'Login.aspx/getCurrentTime',
                    data: '',
                    dataType: 'json',
                    contentType: 'application/json;charset=utf-8',
                    success: function (response) {
                        document.getElementById(id).innerHTML = response.d;
                        setTimeout('datetime("' + id + '");', '1000');
                    },
                    error: function (xhr, ajaxOptions, thrownerror) {
                        alert(xhr.responseText);
                    }
                });
            }
            function date_time(id) {
                date = new Date;
                year = date.getFullYear();
                month = date.getMonth();
                months = new Array('January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December');
                d = date.getDate();
                day = date.getDay();

                days = new Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday');

                h = date.getHours();
                if (h < 10) {
                    h = "0" + h;
                }
                m = date.getMinutes();
                if (m < 10) {
                    m = "0" + m;
                }
                s = date.getSeconds();

                if (s < 10) {
                    s = "0" + s;
                }

                result = '' + days[day] + ' ' + months[month] + ' ' + d + ' ' + year + ' ' + h + ':' + m + ':' + s;
                document.getElementById(id).innerHTML = result;
                setTimeout('date_time("' + id + '");', '1000');
                return true;
            }
        </script>
	</head>

	<body class="preview" data-spy="scroll" data-target=".subnav" data-offset="80">
		<div class="container">
			<header class="jumbotron subhead" id="overview">
				<div class="row">
					<div class="span6">
						<h1 class="ubuntu">AQ Pharmacy</h1>
					</div>
				</div>
			</header>
			<div class="well">
                <h4>Sign In</h4>
				<form id="frm" name="frm" class="form-horizontal" runat="server">
                    <asp:Panel runat="server" ID="divError" CssClass="alert alert-error error-icon" Visible="false">
                        <button type="button" class="close" data-dismiss="alert">x</button>
                        <asp:Label runat="server" ID="lblError"></asp:Label>
                    </asp:Panel>
					<div class="control-group">
						<label class="control-label" for="inputEmail">Login ID</label>
						<div class="controls">
                            <asp:TextBox runat="server" ID="inputEmail" placeholder="Login ID" ClientIDMode="Static" required="true"/>
						</div>
					</div>
					<div class="control-group">
						<label class="control-label" for="inputPassword">Password</label>
						<div class="controls">
                            <asp:TextBox runat="server" ID="inputPassword" TextMode="Password" placeholder="Password" ClientIDMode="Static" required="true"/>
						</div>
					</div>
                    <!--
                    0.Super Administrator
                    1.Administraton
                    2.Doctor
                    3.Cashier
                    4.Pharmacy
                    5.Reception
                    6.Office
                    7.Inventory
                    8.Billing
                    9.Patient
                    -->
					<div class="control-group">
						<div class="controls">
                            <asp:Button runat="server" CssClass="btn" ID="btn" Text="Sign In" OnClick="VerifyLogDetails"/>
						</div>
					</div>
					<div class="control-group">
						<div class="controls">
                        <a href="#" rel="popover" title="Forgot Password" data-content="Please contact Administrator or Vendor and request for resetting password." >Forgot password</a>
						</div>
					</div>
                    <!--
                    <asp:Panel runat="server" ID="pnlWarning" CssClass="alert alert-block" ClientIDMode="Static">
                        <strong>Notes:</strong><br />1. The password is case sensitive.<br />2. Please Logout before closing browser.<br />3. Duplicate Login will not be allowed.<br />4. Every user should have their own Login ID.<br />5. Do not reveal your password to anyone.
                    </asp:Panel>
                    -->
				</form>
			</div>
            <%Response.WriteFile("~/Content/footer.inc"); %>
		</div><!-- /container -->

		<script>
		    function getRemoteIP() {
		        var remoteIPAddress = Request.UserHostAddress;
		        $.ajax({
		            type: 'POST',
		            url: 'Login.aspx/GetClientIPAddress',
		            data: '',
		            dataType: 'json',
		            contentType: 'application/json;charset=utf-8',
		            success: function (response) {
		                alert(response.d);
		            },
		            error: function (xhr, ajaxOptions, thrownerror) {
		                alert(xhr.responseText);
		            }
		        });
		    }
		    $(document).ready(function () {
		        var elements = document.getElementsByTagName("INPUT");
		        for (var i = 0; i < elements.length; i++) {
		            elements[i].oninvalid = function (e) {
		                e.target.setCustomValidity("");
		                if (!e.target.validity.valid) {
		                    switch (e.srcElement.id) {
		                        case "inputEmail":
		                            e.target.setCustomValidity("Login ID cannot be blank");
		                            break;
		                        case "inputPassword":
		                            e.target.setCustomValidity("Password cannot be blank");
		                            break;
		                    }
		                }
		            };
		            elements[i].oninput = function (e) {
		                e.target.setCustomValidity("");
		            };
		        }
		    });
		</script>
	</body>
</html>
