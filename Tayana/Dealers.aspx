<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Dealers.aspx.cs" Inherits="Tayana.Dealers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
    <link href="css/homestyle01.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBackGroundImg" runat="server">
    <div class="banner">
        <ul>
            <li>
                <img src="images/DEALERS.jpg" alt="Tayana Yachts" /></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderOnlyForIndex" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
    <div class="left">
        <div class="left1">
            <p><span>DEALERS</span></p>
            <ul>
                <asp:Repeater runat="server" ID="RepeaterLeft">
                    <ItemTemplate>
                        <li><a href='Dealers.aspx?Id=<%#Eval("Id") %>'><%#Eval("Name") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderRoute" runat="server">
    <a href="#">Home</a> >> <a href="#">Dealers</a> >> <a href="#"><span class="on1">
        <asp:Label runat="server" Text="" ID="labelRoute"></asp:Label>
    </span></a>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    <asp:Literal ID="LiteralTitle" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="box2_list">
        <ul>
            <asp:Repeater ID="RepeaterAgentDetail" runat="server">
                <ItemTemplate>
                    <li>
                        <div class="list02">
                            <ul>
                                <li class="list02li">
                                    <div>
                                        <p>
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# String.IsNullOrEmpty(Eval("CoverPath").ToString()) ?"" : Eval("CoverPath")  %>' />
                                        </p>
                                    </div>
                                </li>
                                <li><span><%#Eval("RegionName") %></span><br />
                                    <%#Eval("Name") %><br />
                                    <%# String.IsNullOrEmpty(Eval("Contact").ToString()) ?"" : $"Contact：{Eval("Contact")}"  %>
<%--                                    <asp:Label ID="Label1" runat="server" Text='<%# String.IsNullOrEmpty(Eval("Contact").ToString()) ?"" : $"Contact：{Eval("Contact")}"  %>'></asp:Label>--%>
                                    <br />
                                    <%# String.IsNullOrEmpty(Eval("Address").ToString()) ?"" : $"Address：{Eval("Address")}"  %>
<%--                                    <asp:Label ID="Label2" runat="server" Text='<%# String.IsNullOrEmpty(Eval("Address").ToString()) ?"" : $"Address：{Eval("Address")}"  %>'></asp:Label>--%>
                                    <br />
                                    <%# String.IsNullOrEmpty(Eval("Tel").ToString()) ?"" : $"TEL：{Eval("Tel")}"  %>
<%--                                    <asp:Label ID="Label3" runat="server" Text='<%# String.IsNullOrEmpty(Eval("Tel").ToString()) ?"" : $"TEL：{Eval("Tel")}"  %>'></asp:Label>--%>
                                    <br />
                                    <%# String.IsNullOrEmpty(Eval("Email").ToString()) ?"" : $"E-mail：{Eval("Email")}"  %>
<%--                                    <asp:Label ID="Label4" runat="server" Text='<%# String.IsNullOrEmpty(Eval("Email").ToString()) ?"" : $"E-mail：{Eval("Email")}"  %>'></asp:Label>--%>
                                    <br />
                                    <a href='<%#Eval("Url") %>' target="_blank"><%#Eval("Url") %></a>
                                </li>
                            </ul>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</asp:Content>
