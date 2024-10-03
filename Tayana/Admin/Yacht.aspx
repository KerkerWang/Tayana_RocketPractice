<%@ Page Title="遊艇管理" Language="C#" MasterPageFile="~/Admin/Site2.Master" AutoEventWireup="true" CodeBehind="Yacht.aspx.cs" Inherits="Tayana.Admin.CreateYacht" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdd" runat="server">
    <%--<h1>遊艇型號管理</h1>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-group">
        <div class="input-group row mx-0">
            <div class="input-group-prepend">
                <span class="input-group-text">遊艇</span>
            </div>
            <asp:TextBox ID="TextBoxNewYacht" class="form-control col-3" runat="server"></asp:TextBox>
            <asp:Button ID="ButtonAddYacht" class="btn btn-primary" runat="server" Text="新增" OnClick="ButtonAddYacht_Click" OnClientClick="javascript:return confirm('確定新增？');" />
        </div>
    </div>
    <div class="row mx-0">
        <span class="text-center"></span>



    </div>
    <div class="table-responsive">
        <asp:GridView ID="GridViewYacht" class="table align-items-center table-dark" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowEditing="GridViewYacht_RowEditing" OnRowCancelingEdit="GridViewYacht_RowCancelingEdit" OnRowUpdating="GridViewYacht_RowUpdating" OnRowDeleting="GridViewYacht_RowDeleting" OnDataBound="GridViewYacht_DataBound" OnRowCommand="GridViewYacht_RowCommand" OnRowDataBound="GridViewYacht_RowDataBound">
            <HeaderStyle CssClass="" />
            <Columns>
                <asp:TemplateField HeaderText="Cover Image" InsertVisible="False">
                    <ItemTemplate>
                        <asp:Image ID="ImageCoverPath" class="img-fluid rounded" runat="server" ImageUrl='<%#Eval("CoverPath") %>' />
                        <br />
                        <asp:DropDownList ID="DropDownListYachtGallery" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListYachtGallery_SelectedIndexChanged"></asp:DropDownList>
                        <asp:Button ID="ButtonCoverPathUpdate" class="btn btn-secondary" runat="server" Text="更換封面" CommandName="CoverPathUpdate" CommandArgument='<%#Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxYachtName" class="form-control" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelYachtName" class="h3 text-secondary" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="最新設計" SortExpression="Is_NewDesigned">
                    <EditItemTemplate>
                        <asp:CheckBox ID="CheckBoxIsNewDesignedEdit" runat="server" Checked='<%# Bind("Is_NewDesigned") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBoxIsNewDesigned" runat="server" Checked='<%# Bind("Is_NewDesigned") %>' Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="最新建造" SortExpression="Is_NewBuilt">
                    <EditItemTemplate>
                        <asp:CheckBox ID="CheckBoxIsNewBuiltEdit" runat="server" Checked='<%# Bind("Is_NewBuilt") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBoxIsNewBuilt" runat="server" Checked='<%# Bind("Is_NewBuilt") %>' Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButtonYachtUpdate" class="btn btn-secondary" runat="server" CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButtonYachtUpdateCancel" class="btn btn-secondary" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonYachtEdit" class="btn btn-secondary" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonYachtDelete" class="btn btn-danger" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="javascript:return confirm('確定刪除？');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
