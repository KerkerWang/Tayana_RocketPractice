<%@ Page Title="Yacht頁面管理" Language="C#" MasterPageFile="~/Admin/Site2.Master" AutoEventWireup="true" CodeBehind="YachtContent.aspx.cs" Inherits="Tayana.Admin.YachtContent" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdd" runat="server">
    <asp:Repeater ID="RepeaterYachtTypeButtons" runat="server" OnItemCommand="RepeaterYachtTypeButtons_ItemCommand">
        <ItemTemplate>
            <asp:Button ID="ButtonChooseYacht" class="btn btn-secondary" runat="server" Text='<%#Eval("Name") %>' CommandArgument='<%#Eval("Id") %>' CommandName="ChooseYacht" PostBackUrl='<%#"YachtContent.aspx?id="+Eval("Id") %>' />
        </ItemTemplate>
    </asp:Repeater>
    <asp:Literal ID="LiteralTitle" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DropDownList ID="DropDownListYachtContentType" class="form-control" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="DropDownListYachtContentType_SelectedIndexChanged">
        <asp:ListItem Text="編輯Overview文字內容" Value="1"></asp:ListItem>
        <asp:ListItem Text="編輯Dimension內容" Value="2"></asp:ListItem>
        <asp:ListItem Text="編輯Specification內容" Value="3"></asp:ListItem>
        <asp:ListItem Text="上傳圖片及檔案" Value="4"></asp:ListItem>
    </asp:DropDownList>
    <br />
    <asp:Panel ID="PanelOverview" runat="server" Visible="false">
        <CKEditor:CKEditorControl ID="CKEditorControlOverview" runat="server" BasePath="/Scripts/ckeditor/"></CKEditor:CKEditorControl>
        <asp:Button ID="ButtonOverviewSave" class="btn btn-primary" runat="server" Text="存檔" OnClick="ButtonOverviewSave_Click" />
    </asp:Panel>
    <asp:Panel ID="PanelDimension" runat="server" Visible="false">
        <div class="form-group">
            <div class="input-group row mx-0">
                <div class="input-group-prepend">
                    <span class="input-group-text">DimensionKey</span>
                </div>
                <asp:TextBox ID="TextBoxDimensionKey" class="form-control col-3" runat="server"></asp:TextBox>
                <div class="input-group-prepend">
                    <span class="input-group-text">DimensionValue</span>
                </div>
                <asp:TextBox ID="TextBoxDimensionValue" class="form-control col-3" runat="server"></asp:TextBox>
                <asp:Button ID="ButtonAddDimension" class="btn btn-primary" runat="server" Text="新增" OnClientClick="javascript:return confirm('確定新增？');" OnClick="ButtonAddDimension_Click" />
            </div>
        </div>
        <asp:GridView ID="GridViewDimension" class="table align-items-center table-dark" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowEditing="GridViewDimension_RowEditing" OnRowCancelingEdit="GridViewDimension_RowCancelingEdit" OnRowUpdating="GridViewDimension_RowUpdating" OnRowDeleting="GridViewDimension_RowDeleting">
            <Columns>
                <asp:TemplateField HeaderText="DimensionKey" SortExpression="DimensionKey">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxDimensionKey" class="form-control" runat="server" Text='<%# Bind("DimensionKey") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelDimensionKey" runat="server" Text='<%# Bind("DimensionKey") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DimensionValue" SortExpression="DimensionValue">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxDimensionValue" class="form-control" runat="server" Text='<%# Bind("DimensionValue") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelDimensionValue" runat="server" Text='<%# Bind("DimensionValue") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButtonDimensionUpdate" class="btn btn-secondary" runat="server" CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButtonDimensionEditCancel" class="btn btn-secondary" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonDimensionEdit" class="btn btn-secondary" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonDimensionDelete" class="btn btn-danger" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="javascript:return confirm('確定刪除？');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="PanelSpecification" runat="server" Visible="false">
        <h2>Specification種類</h2>
        <div class="form-group">
            <div class="input-group row mx-0">
                <div class="input-group-prepend">
                    <span class="input-group-text">種類</span>
                </div>
                <asp:TextBox ID="TextBoxSpecificationType" class="form-control col-3" runat="server"></asp:TextBox>
                <asp:Button ID="ButtonSpecificationTypeAdd" class="btn btn-primary" runat="server" Text="新增種類" OnClick="ButtonSpecificationTypeAdd_Click" OnClientClick="javascript:return confirm('確定新增？');" />
            </div>
        </div>
        <asp:GridView ID="GridViewSpecificationType" class="table align-items-center table-dark" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowEditing="GridViewSpecificationType_RowEditing" OnRowCancelingEdit="GridViewSpecificationType_RowCancelingEdit" OnRowUpdating="GridViewSpecificationType_RowUpdating" OnRowDeleting="GridViewSpecificationType_RowDeleting">
            <Columns>
                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxSpecificationTypeName" class="form-control" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelSpecificationTypeName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButtonSpecificationTypeNameUpdate" class="btn btn-secondary" runat="server" CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButtonSpecificationTypeNameEditCancel" class="btn btn-secondary" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonSpecificationTypeNameEdit" class="btn btn-secondary" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonSpecificationTypeNameDelete" class="btn btn-danger" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="javascript:return confirm('確定刪除？');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <h2>Specification細項</h2>
        <div class="form-group">
            <div class="input-group row mx-0">
                <asp:DropDownList ID="DropDownListSpecificationItem" class="form-control col-2" runat="server" OnSelectedIndexChanged="DropDownListSpecificationItem_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                <div class="input-group-prepend">
                    <span class="input-group-text">種類</span>
                </div>
                <asp:TextBox ID="TextBoxSpecificationItem" class="form-control col-3" runat="server"></asp:TextBox>
                <asp:Button ID="ButtonSpecificationItemAdd" class="btn btn-primary" runat="server" Text="新增細項" OnClick="ButtonSpecificationItemAdd_Click" OnClientClick="javascript:return confirm('確定新增？');" />
            </div>
        </div>
        <asp:GridView ID="GridViewSpecificationItem" class="table align-items-center table-dark" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowEditing="GridViewSpecificationItem_RowEditing" OnRowCancelingEdit="GridViewSpecificationItem_RowCancelingEdit" OnRowUpdating="GridViewSpecificationItem_RowUpdating" OnRowDeleting="GridViewSpecificationItem_RowDeleting">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ReadOnly="True" />
                <asp:TemplateField HeaderText="Content" SortExpression="Content">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxSpecificationContent" class="form-control" runat="server" Text='<%# Bind("Content") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelSpecificationContent" runat="server" Text='<%# Bind("Content") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButtonSpecificationContentUpdate" class="btn btn-secondary" runat="server" CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButtonSpecificationContentEditCancel" class="btn btn-secondary" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonSpecificationContentEdit" class="btn btn-secondary" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonSpecificationContentDelete" class="btn btn-danger" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="javascript:return confirm('確定刪除？');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="PanelUpload" runat="server" Visible="false">
        <h2>新增或修改Overview相片</h2>
        <div class="input-group row mx-0">
            <asp:FileUpload ID="FileUploadDimensionPhoto" class="form-control col-4" runat="server" />
            <asp:Button ID="ButtonUploadDimensionPhoto" class="btn btn-primary" runat="server" Text="上傳相片" OnClick="ButtonUploadDimensionPhoto_Click" OnClientClick="javascript:return confirm('確定上傳？');" />
        </div>
        <asp:GridView ID="GridViewDimensionPhoto" class="table align-items-center table-dark" runat="server" AutoGenerateColumns="False" DataKeyNames="Id">
            <Columns>
                <asp:ImageField DataImageUrlField="DimensionPhotoPath" HeaderText="OverviewPhoto"></asp:ImageField>
            </Columns>
        </asp:GridView>
        <br />
        <h2>新增或修改Overview檔案</h2>
        <div class="input-group row mx-0">
            <asp:FileUpload ID="FileUploadDownload" class="form-control col-4" runat="server" />
            <asp:Button ID="ButtonUploadDownload" class="btn btn-primary" runat="server" Text="上傳檔案" OnClick="ButtonUploadDownload_Click" OnClientClick="javascript:return confirm('確定上傳？');" />
        </div>
        <asp:GridView ID="GridViewDownload" class="table align-items-center table-dark" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowEditing="GridViewDownload_RowEditing" OnRowCancelingEdit="GridViewDownload_RowCancelingEdit" OnRowUpdating="GridViewDownload_RowUpdating">
            <Columns>
                <asp:TemplateField HeaderText="DownloadFileName" SortExpression="DownloadFileName">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxDownloadFileName" class="form-control" runat="server" Text='<%# Bind("DownloadFileName") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelDownloadFileName" runat="server" Text='<%# Bind("DownloadFileName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButtonDownloadUpdate" class="btn btn-secondary" runat="server" CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButtonDownloadEditCancel" class="btn btn-secondary" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonDownloadEdit" class="btn btn-secondary" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <h2>上傳Layout相片</h2>
        <div class="input-group row mx-0">
            <asp:FileUpload ID="FileUploadLayoutPhoto" class="form-control col-4" runat="server" AllowMultiple="True" />
            <asp:Button ID="ButtonUploadLayoutPhoto" class="btn btn-primary" runat="server" Text="上傳相片" OnClick="ButtonUploadLayoutPhoto_Click" OnClientClick="javascript:return confirm('確定新增？');" />
        </div>
        <asp:GridView ID="GridViewLayoutPhoto" class="table align-items-center table-dark" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowDeleting="GridViewLayoutPhoto_RowDeleting">
            <Columns>
                <asp:ImageField DataImageUrlField="PhotoPath" HeaderText="LayoutPhoto"></asp:ImageField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonLayoutPhotoDelete" class="btn btn-danger" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="javascript:return confirm('確定刪除？');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <h2>上傳Gallery相片</h2>
        <div class="input-group row mx-0">
            <asp:FileUpload ID="FileUploadGalleryPhoto" class="form-control col-4" runat="server" AllowMultiple="True" />
            <asp:Button ID="ButtonUploadGalleryPhoto" class="btn btn-primary" runat="server" Text="上傳相片" OnClientClick="javascript:return confirm('確定上傳？');" OnClick="ButtonUploadGalleryPhoto_Click" />
        </div>
        <asp:GridView ID="GridViewGalleryPhoto" class="table align-items-center table-dark" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowDeleting="GridViewGalleryPhoto_RowDeleting">
            <Columns>
                <asp:ImageField DataImageUrlField="PhotoPath" HeaderText="GalleryPhoto"></asp:ImageField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonGalleryPhotoDelete" class="btn btn-danger" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="javascript:return confirm('確定刪除？');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
