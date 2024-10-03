<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="Tayana.Company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
    <link href="css/homestyle01.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBackGroundImg" runat="server">
    <div class="banner">
        <ul>
            <li>
                <img src="images/newbanner.jpg" alt="Tayana Yachts" /></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderOnlyForIndex" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
    <div class="left">
        <div class="left1">
            <p><span>COMPANY</span></p>
            <ul>
                <asp:Repeater runat="server" ID="RepeaterLeft">
                    <ItemTemplate>
                        <li><a href='Company.aspx?id=<%#Eval("Id") %>'><%#Eval("Name") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderRoute" runat="server">
    <a href="#">Home</a> >> <a href="Company.aspx">Company</a> >> <a href="#"><span class="on1">
        <asp:Literal ID="LiteralRoute" runat="server"></asp:Literal>
    </span></a>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    <asp:Literal ID="LiteralTitle" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:Literal ID="LiteralContent" runat="server"></asp:Literal>
</asp:Content>
