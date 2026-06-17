<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="IWPS.login" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>IWPS Login - BHEL</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.1.4/toastr.min.css" />
    <style>
        body { background: linear-gradient(135deg, #002244 0%, #003366 50%, #004d99 100%); min-height: 100vh; font-family: Cambria, Georgia, serif; display: flex; align-items: center; justify-content: center; }
        .login-card { background: white; border-radius: 10px; box-shadow: 0 20px 60px rgba(0,0,0,0.3); overflow: hidden; width: 400px; }
        .login-header { background: linear-gradient(135deg, #003366, #004d99); padding: 30px; text-align: center; }
        .login-header img { height: 70px; margin-bottom: 10px; }
        .login-header h6 { color: #add8e6; font-size: 11px; margin-bottom: 5px; }
        .login-header h5 { color: white; font-size: 14px; margin: 0; }
        .login-body { padding: 30px; }
        .login-title { color: #003366; font-weight: bold; font-size: 18px; text-align: center; margin-bottom: 25px; }
        .input-group-text { background: #003366; color: white; border: 1px solid #003366; }
        .form-control { border: 1px solid #003366; }
        .btn-login { background: #003366; color: white; width: 100%; padding: 10px; font-size: 16px; border: none; border-radius: 4px; margin-top: 10px; }
        .btn-login:hover { background: #004d99; color: white; }
        .error-msg { color: red; font-size: 13px; text-align: center; margin-top: 10px; }
    </style>
</head>
<body>
<form id="form1" runat="server">
<div class="login-card">
    <div class="login-header">
        <img src="content/bhel.png" alt="BHEL" onerror="this.style.display='none'" />
        <h6>भारत हेवी इलेक्ट्रिकल्स लिमिटेड</h6>
        <h5>Bharat Heavy Electricals Limited</h5>
    </div>
    <div class="login-body">
        <div class="login-title">IWPS Login</div>
        <div class="form-group">
            <label class="font-weight-bold" style="color:#003366;">Staff No</label>
            <div class="input-group">
                <div class="input-group-prepend"><span class="input-group-text"><i class="fa fa-user">&#128100;</i></span></div>
                <asp:TextBox ID="txtStaffNo" runat="server" CssClass="form-control" placeholder="Enter Staff No"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="font-weight-bold" style="color:#003366;">Password</label>
            <div class="input-group">
                <div class="input-group-prepend"><span class="input-group-text">&#128274;</span></div>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter Password"></asp:TextBox>
            </div>
        </div>
        <asp:Button ID="btnLogin" runat="server" Text="LOGIN" CssClass="btn-login" OnClick="btnLogin_Click" />
        <asp:Label ID="lblError" runat="server" CssClass="error-msg" Text="" Visible="false"></asp:Label>
        <div style="margin-top:15px;text-align:center;font-size:11px;color:#999;">Demo: STAFF01/bhel123 (NOR) | APP01/bhel123 (APP)</div>
    </div>
</div>
</form>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.1.4/toastr.min.js"></script>
</body>
</html>
