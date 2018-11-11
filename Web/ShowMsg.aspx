<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowMsg.aspx.cs" Inherits="BookShop.Web.ShowMsg" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        window.onload = function () {
            setTimeout(changeTime, 1000);
        }
        function changeTime() {
            var time = document.getElementById("time").innerHTML;
            time = parseInt(time);
            time--;
            if (time <= 0) {
                var url = document.getElementById("url").href;
                window.location = url;
            } else {
                document.getElementById("time").innerHTML= time;
                setTimeout(changeTime, 1000);
            }
        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>  
      <table width="490" height="325" border="0" align="center" cellpadding="0" cellspacing="0" background="Images/showinfo.png">
      <tr>
        <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="50">&nbsp;</td>
            <td>&nbsp;</td>
            <td width="40">&nbsp;</td>
          </tr>
          <tr>
            <td width="50">&nbsp;</td>
            <td style="text-align: center">
             <%=this.msg %>
              </td>
            <td width="40">&nbsp;</td>
          </tr>
          <tr>
            <td width="50">&nbsp;</td>
            <td>&nbsp;</td>
            <td width="40">&nbsp;</td>
          </tr>
          <tr>
            <td width="50" class="style1">&nbsp;</td>
            <td style="text-align: center">
               <span style="font-size:18px;color:Red" id="time">5</span>秒钟以后跳转到 <a href="<%=this.url %>" id="url"><%=this.txt %></a>

                                </td>
            <td width="40">&nbsp;</td>
          </tr>
        </table></td>
      </tr>
    </table>
    </div>
    </form>
</body>
</html>
