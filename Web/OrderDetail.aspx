<%@ Page Title="" Language="C#" MasterPageFile="~/BuyMaster.Master" AutoEventWireup="true" EnableViewState="false" CodeBehind="OrderDetail.aspx.cs" Inherits="BookShop.Web.OrderDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(
        function () {

            $(".m_r").css("width", "710px");
            $(".m_r").css("float", "none");

        }

        );

        function OnGoPay(control) {
            alert("请到我的定单中付款!");
        }

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <div style="border:solid 4px #aacded;width:710px" >
    <div style="background:#AACDED; text-align:left"><h2>订单详细</h2></div>
    <div style="background:#FFFFF7; text-align:left;width:710px">
     <b>当前定单状态:</b><%=this.GetState(oneOrder.State) %><br /><br />
     <b>邮寄地址:</b><%=oneOrder.PostAddress %>
    </div>
    <hr style="border-style:dashed; width:100%; border-color:#ccc"  />
     <div align="left">
     
              <asp:Repeater ID="rptDetails" runat="server">
              <HeaderTemplate>
                            
            <table datasrc="" cellspacing="0" cellpadding="1" width="100%" border="1">
              <tbody>
                <tr class="align_Center Thead">
                  <td width="10%">图书编号</td>
                  <td>商品名称</td>
                  <td width="10%">单价</td>
                  <td width="8%">数量</td>
                      
                </tr>
              </HeaderTemplate>
              <ItemTemplate>
                <tr class="align_Center">
                  <td style="PADDING-BOTTOM: 5px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; PADDING-TOP: 5px"><%#Eval("Book.Id") %></td>
                  <td class=align_Left><a onmouseover="" onmouseout="" onclick="" href='<%#Eval("Book.Id","book.aspx?id={0}") %>' target="_blank" ><%#Eval("Book.Title") %></a>
                      </td>
                  <td><span class="price">￥<%#Eval("UnitPrice","{0:0.00}") %></span></td>
                  <td><%#Eval("Quantity") %></td>
          
            
                </tr>
                </ItemTemplate>
                <FooterTemplate>
              </tbody>
            </table>
            </FooterTemplate>
            </asp:Repeater>
     </div>
</div>
</asp:Content>
