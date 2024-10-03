<%@ Page Title="新增News頁面" Language="C#" MasterPageFile="~/Admin/Site2.Master" AutoEventWireup="true" CodeBehind="AddNews.aspx.cs" Inherits="Tayana.Admin.NewsGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdd" runat="server">
    <asp:Button ID="Button1" class="btn btn-secondary" runat="server" Text="返回News管理頁面" PostBackUrl="~/Admin/News.aspx"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="input-group row mx-0">
        <div class="input-group-prepend">
            <span class="input-group-text">Title&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
        </div>
        <asp:TextBox ID="TextBoxTitle" class="form-control col-3" runat="server"></asp:TextBox>
    </div>
    <br />
    <div class="input-group row mx-0">
        <div class="input-group-prepend">
            <span class="input-group-text">Subtitle</span>
        </div>
        <asp:TextBox ID="TextBoxSubtitle" class="form-control col-3" runat="server"></asp:TextBox>
    </div>
    <br />
    <div class="input-group row mx-0">
        <div class="input-group-prepend">
            <span class="input-group-text">Content</span>
        </div>
        <asp:TextBox ID="TextBoxContent" class="form-control col-3" runat="server"></asp:TextBox>
    </div>
    <asp:Button ID="ButtonAddNews" class="btn btn-primary" runat="server" Text="新增" OnClientClick="javascript:return confirm('確定新增？');" OnClick="ButtonAddNews_Click" />
</asp:Content>
