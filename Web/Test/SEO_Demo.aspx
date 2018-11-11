<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SEO_Demo.aspx.cs" Inherits="BookShop.Web.Test.SEO_Demo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../js/jquery-1.7.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#a1").click(function () {
                $.post("/ashx/Seo.ashx", { "action": "a1" }, function (data) {
                    $("#div1").text(data);
                   
                })
                return false;
            });
        });
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <a href="/ashx/Seo.ashx?action=a1"  id="a1">新闻标题1</a>
    </div>

    <div id="div1">
    
    </div>
    </form>
</body>
</html>
