<%@ Page Title="" Language="C#" MasterPageFile="~/member/RegMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BookShop.Web.member.Login" %>
<%@ Register src="ULogin.ascx" tagname="ULogin" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ULogin ID="ULogin1" runat="server" />
    
   
</asp:Content>
