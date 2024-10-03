<%@ Page Title="News相簿頁面" Language="C#" MasterPageFile="~/Admin/Site2.Master" AutoEventWireup="true" CodeBehind="NewsGallery.aspx.cs" Inherits="Tayana.Admin.News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdd" runat="server">
    <asp:Button ID="Button1" class="btn btn-secondary" runat="server" Text="返回News管理頁面" PostBackUrl="~/Admin/News.aspx" />
    <asp:Literal ID="Literalh2" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="input-group row mx-0">
        <asp:FileUpload ID="FileUploadGallery" class="form-control col-4" runat="server" AllowMultiple="True" />
        <asp:Button ID="ButtonUpload" class="btn btn-primary" runat="server" Text="上傳" OnClick="ButtonUpload_Click" />
    </div>
    <asp:GridView ID="GridViewNewsGallery" class="table align-items-center table-dark" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowEditing="GridViewNewsGallery_RowEditing" OnRowCancelingEdit="GridViewNewsGallery_RowCancelingEdit" OnRowUpdating="GridViewNewsGallery_RowUpdating" OnRowDeleting="GridViewNewsGallery_RowDeleting">
        <Columns>
            <asp:ImageField DataImageUrlField="PhotoPath" HeaderText="Photo" InsertVisible="False" ReadOnly="True"></asp:ImageField>
            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxNewsGalleryName" class="form-control" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelNewsGalleryName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButtonNewsGalleryUpdate" class="btn btn-secondary" runat="server" CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButtonNewsGalleryEditCancel" class="btn btn-secondary" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonNewsGalleryEdit" class="btn btn-secondary" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonNewsGalleryDelete" class="btn btn-danger" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
