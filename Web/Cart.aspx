<%@ Page Title="" Language="C#" MasterPageFile="~/BuyMaster.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="BookShop.Web.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#dialog-message").dialog({
                autoOpen: false, //隐藏DIV
                modal: true,
                buttons: {
                    Ok: function () {
                        $(this).dialog('close');
                    }
                }
            });
            getTotalMoney();
        });
        //弹出
        function ShowDialog() {
            $("#dialog-message").dialog('open');
        }

//更新数量
        function changeBar(operator, pkId, bookId) {
            $control = $("#txtCount" + bookId); //找到数量文本框.
            var count = $control.val(); //获取数量.
            if (operator == '+') {//加法
                count = parseInt(count) + 1;
            } else if (operator == '-') {
                count = parseInt(count) - 1;
                if (count < 1) {
                    //alert("数量最少为1");
                    $("#message").text("数量最少为1");
                    ShowDialog();
                    return;
                }
            }
            $.post("/ashx/ProcessCart.ashx", { "action": "change", "pkId": pkId, "count": count }, function (data) {
                if (data == "yes") {
                    //文本框中的数量更新。
                    $control.val(count);
                    //更新购物车商品的总价
                    getTotalMoney();
                } else {

                }
            });

        }
    //删除购物车中的商品项
        function removeProductOnShoppingCart(pkId,control) {
            if (confirm("确定要删除吗?")) {
                $.post("/ashx/ProcessCart.ashx", { "action": "del", "pkId": pkId }, function (data) {
                    if (data == "yes") {
                        //将该项从购物车表格中移除.
                        $(control).parent().parent().remove();
                        //更新购物车中商品的总价
                        getTotalMoney();
                    }
                });
            }
        }
        //更新商品数量
        function changeTextOnBlur(pkId, control) {
            var count = $(control).val();
            var reg = /^\d{1,3}$/;
            if (reg.test(count)) {
                //异步请求更新数据库中的商品的数量.
                //更新价格。
                getTotalMoney();
            } else {
                $("#message").text("数量只能是数字");
                ShowDialog();
                $(control).val(inputValue);//还原回文本框默认显示的数字。

            }

        }
        //文本框获得焦点时记录文本框中的值。
        var inputValue;
        function changeTxtOnFocus(control) {
             inputValue = $(control).val();
         }
         //计算商品总价
         function getTotalMoney() {
             var totalMoney = 0;//注意要赋初始值。
         //找到商品所在的行，开始遍历
             $(".align_Center:gt(0)").each(function () {
                 var price = $(this).find(".price").text(); //单价
                 var count = $(this).find("input").val(); //数量.
                 totalMoney = totalMoney + (parseFloat(price) * parseInt(count));

             });
             $("#totleMoney").text(totalMoney);
         }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0"  width="98%">
        <tr>
            <td colspan="2">
                <img height="27" 
                    src="images/shop-cart-header-blue.gif" width="206" /><img alt="" 
                    src="Images/png-0170.png" /><asp:HyperLink ID="HyperLink1" runat="server" 
                    NavigateUrl="~/myorder.aspx">我的订单</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" width="98%">
          
                <table  cellpadding='0' cellspacing='0' width='100%' >
 <tr class='align_Center Thead'>
    <td width='7%' style='height:30px'>图片</td>
    <td>图书名称</td>
    <td width='14%'>单价</td>
    <td width='11%'>购买数量</td>
    <td width='7%'>删除图书</td>
 </tr>
    
                    <asp:Repeater ID="CartRepeater" runat="server">
                    <ItemTemplate>
                    <!--一行数据的开始 -->
<tr class='align_Center'>
   <td style='padding:5px 0 5px 0;'><img src='<%#Eval("Book.ISBN","/Images/BookCovers/{0}.jpg") %>' width="40" height="50" border="0" /></td>
   <td class='align_Left'><%#Eval("Book.Title") %></td>
   <td>
<span class='price'><%#Eval("Book.UnitPrice","{0:0.00}")%></span>
</td>
   <td><a href='#none' title='减一' onclick="changeBar('-',<%#Eval("Id") %>,<%#Eval("Book.Id") %>)" style='margin-right:2px;' ><img src="Images/bag_close.gif" width="9" height="9" border='none' style='display:inline' /></a>
     <input type='text' id='txtCount<%#Eval("Book.Id") %>' name='txtCount<%#Eval("Book.Id") %>' maxlength='3' style='width:30px' onKeyDown='if(event.keyCode == 13) event.returnValue = false' value='<%#Eval("Count") %>' onfocus='changeTxtOnFocus(this);' onblur="changeTextOnBlur(<%#Eval("Id") %>,this);" />
     <a href='#none' title='加一' onclick="changeBar('+',<%#Eval("Id") %>,<%#Eval("Book.Id") %>)" style='margin-left:2px;' ><img src='/images/bag_open.gif' width="9" height="9" border='none' style='display:inline' /></a>   </td>
   <td>
   <a href='#none' id='btn_del_1000357315' onclick="removeProductOnShoppingCart(<%#Eval("Id") %>,this)" >
       删除</a>
       
       </td>
</tr>
<!--一行数据的结束 -->
                    
                    </ItemTemplate>
                    </asp:Repeater>

 <tr>
    <td class='align_Right Tfoot' colspan='5' style='height:30px'>&nbsp;</td>
 </tr>
</table>
</td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;&nbsp;&nbsp; 商品金额总计：<span ID="totleMoney" 
                   >0</span>元</td>
            <td>
                &nbsp;
               <a href="booklist.aspx"> <img alt="" src="Images/gobuy.jpg" width="103" height="36" border="0" /> </a><a href="OrderConfirm.aspx"><img src="images/balance.gif" 
                     border="0" /></a>
              
            </td>
        </tr>
    </table>
    </div>
    <div id="dialog-message" title="注意警告!!">
	<p>
		<span id="message" style="font-size:14px;color:Red"></span>
        
	</p>
	
</div>

</asp:Content>
