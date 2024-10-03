<%@ Page Title="News頁面管理" Language="C#" MasterPageFile="~/Admin/Site2.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="Tayana.Admin.AddNews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdd" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="新增News" PostBackUrl="~/Admin/AddNews.aspx" />
    <div class="table-responsive">
        <asp:GridView ID="GridViewNews" class="table align-items-center table-dark" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnDataBound="GridViewNews_DataBound" OnRowEditing="GridViewNews_RowEditing" OnRowCancelingEdit="GridViewNews_RowCancelingEdit" OnRowUpdating="GridViewNews_RowUpdating" OnRowDeleting="GridViewNews_RowDeleting" OnRowCommand="GridViewNews_RowCommand" OnRowDataBound="GridViewNews_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Cover Image" InsertVisible="False">
                    <ItemTemplate>
                        <asp:Image ID="ImageCoverPath" class="img-fluid rounded" runat="server" ImageUrl='<%#Eval("CoverPath") %>' />
                        <br />
                        <asp:DropDownList ID="DropDownListNewsGallery" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListNewsGallery_SelectedIndexChanged"></asp:DropDownList>
                        <asp:Button ID="ButtonCoverPathUpdate" class="btn btn-secondary" runat="server" Text="更換封面" CommandName="CoverPathUpdate" CommandArgument='<%#Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="置頂">
                    <EditItemTemplate>
                        <asp:CheckBox ID="CheckBoxNewsIsTopEdit" runat="server" Checked='<%# Bind("Is_Top") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBoxNewsIsTop" runat="server" Checked='<%# Bind("Is_Top") %>' Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title" SortExpression="Title">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxNewsTitle" class="form-control" runat="server" Text='<%# Bind("Title") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelNewsTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subtitle" SortExpression="Subtitle">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxNewsSubtitle" class="form-control" runat="server" Text='<%# Bind("Subtitle") %>' TextMode="MultiLine"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelNewsSubtitle" style="word-break:break-all; word-wrap:break-word;" runat="server" Text='<%# Bind("Subtitle") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Content" SortExpression="Content">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxNewsContent" class="form-control" runat="server" Text='<%# Bind("Content") %>' TextMode="MultiLine"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelNewsContent" runat="server" Text='<%# Bind("Content") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButtonNewsUpdate" class="btn btn-secondary" runat="server" CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButtonNewsEditCancel" class="btn btn-secondary" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonNewsEdit" class="btn btn-secondary" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonNewsDelete" class="btn btn-danger" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="javascript:return confirm('確定刪除？');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="ButtonNewsGallery" class="btn btn-success" runat="server" Text="相簿" PostBackUrl='<%#"~/Admin/NewsGallery.aspx?Id=" + Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
