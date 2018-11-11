<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false"  CodeBehind="Default.aspx.cs" Inherits="BookShop.Web.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ê×Ò³</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       ·þÎñ¶Ë   <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
       HTML <input type="text" name="txtName" value="<%=value %>" />
      <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
    </div>
    </form>
</body>
</html>
