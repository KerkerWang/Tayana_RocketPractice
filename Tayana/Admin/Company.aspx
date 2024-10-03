<%@ Page Title="Company頁面管理" Language="C#" MasterPageFile="~/Admin/Site2.Master" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="Tayana.Admin.Company" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdd" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DropDownList ID="DropDownListCompanyPage" class="form-control" runat="server" OnSelectedIndexChanged="DropDownListCompanyPage_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    <br />
    <CKEditor:CKEditorControl ID="CKEditorControl1" runat="server" BasePath="/Scripts/ckeditor/"></CKEditor:CKEditorControl>
    <br />
    <asp:Button ID="ButtonSave" class="btn btn-primary" runat="server" Text="儲存" OnClick="ButtonSave_Click" />
</asp:Content>
