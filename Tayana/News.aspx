<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="Tayana.News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
    <link href="css/homestyle01.css?v=042601" rel="stylesheet" type="text/css" />
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
            <p><span>NEWS</span></p>
            <ul>
                <li><a href="News.aspx">News & Events</a></li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderRoute" runat="server">
    <a href="#">Home</a> >> <a href="News.aspx">News </a>>> <a href="News.aspx"><span class="on1">News & Events</span></a>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    News & Events
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="box2_list">
        <ul>
            <asp:Repeater runat="server" ID="RepeaterMain">
                <ItemTemplate>
                    <li>
                        <div class="list01">
                            <ul>
                                <li>
                                    <div>
                                        <p>
                                            <img src='<%#Eval("CoverPath") %>' border="0" />
                                        </p>
                                    </div>
                                </li>
                                <li><span><%# $"{Eval("CreateTime"):yyyy/MM/dd}" %></span><br />
                                    <a href='<%# "News.aspx?Id=" + Eval("Id")%>'><%#Eval("Title") %></a></li>
                                <br />
                                <li><%#Eval("Subtitle") %></li>
                            </ul>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <div class="pagenumber">
            <asp:Literal ID="LiteralPages" runat="server"></asp:Literal></div>
        <div class="pagenumber1">Items：<span><asp:Literal ID="LiteralItemCount" runat="server"></asp:Literal></span>  |  Pages：<span><asp:Literal ID="LiteralPageNow" runat="server"></asp:Literal></span></div>


    </div>
</asp:Content>
