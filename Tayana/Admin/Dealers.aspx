<%@ Page Title="Dealers頁面管理" Language="C#" MasterPageFile="~/Admin/Site2.Master" AutoEventWireup="true" CodeBehind="Dealers.aspx.cs" Inherits="Tayana.Admin.Dealers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 152px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdd" runat="server">
    <asp:Button ID="ButtonCountry" class="btn btn-secondary" runat="server" Text="Country" OnClick="ButtonCountry_Click" />
    <asp:Button ID="ButtonRegion" class="btn btn-secondary" runat="server" Text="Region" OnClick="ButtonRegion_Click" />
    <asp:Button ID="ButtonAgent" class="btn btn-secondary" runat="server" Text="Agent" OnClick="ButtonAgent_Click" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="PanelCountry" runat="server" Visible="false">
        <div class="form-group">
            <div class="input-group row mx-0">
                <div class="input-group-prepend">
                    <span class="input-group-text">國家</span>
                </div>
                <asp:TextBox ID="TextBoxCountry" class="form-control col-3" runat="server"></asp:TextBox>
                <asp:Button ID="ButtonAddCountry" class="btn btn-primary" runat="server" Text="新增" OnClick="ButtonAddCountry_Click" OnClientClick="javascript:return confirm('確定新增？');" />
            </div>
        </div>
        <asp:GridView ID="GridViewCountry" class="table align-items-center table-dark" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowDeleting="GridViewCountry_RowDeleting" OnRowEditing="GridViewCountry_RowEditing" OnRowUpdating="GridViewCountry_RowUpdating" OnRowCancelingEdit="GridViewCountry_RowCancelingEdit">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxCountryName" class="form-control" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--                <asp:BoundField DataField="CreateTime" HeaderText="CreateTime" SortExpression="CreateTime" InsertVisible="False" ReadOnly="True" />--%>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButtonCountryUpdate" class="btn btn-secondary" runat="server" CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButtonCountryUpdateCancel" class="btn btn-secondary" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonLinkButtonCountryEdit" class="btn btn-secondary" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonCountryDelete" class="btn btn-danger" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="javascript:return confirm('確定刪除？');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="PanelRegion" runat="server" Visible="false">
        <div class="row">
            <div class="form-group col-2 pr-0">
                <div class="input-group row mx-0">
                    <div class="input-group-prepend">
                        <span class="input-group-text">國家</span>
                    </div>
                    <asp:DropDownList ID="DropDownListCountry" class="form-control" runat="server" OnSelectedIndexChanged="DropDownListCountry_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
            <div class="form-group col-3 pl-0">
                <div class="input-group row mx-0">
                    <div class="input-group-prepend">
                        <span class="input-group-text">地區</span>
                    </div>
                    <asp:TextBox ID="TextBoxRegion" class="form-control" runat="server"></asp:TextBox>
                    <asp:Button ID="ButtonAddRegion" class="btn btn-primary" runat="server" Text="新增" OnClick="ButtonAddRegion_Click" OnClientClick="javascript:return confirm('確定新增？');" />
                </div>
            </div>
        </div>
        <br />
        <div class="form-group">
            <div class="input-group row mx-0">
                <div class="input-group-prepend">
                    <span class="input-group-text">選擇國家</span>
                </div>
                <asp:DropDownList ID="DropDownListGridViewRegion" class="form-control col-3" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListGridViewRegion_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <asp:GridView ID="GridViewRegion" class="table align-items-center table-dark" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowEditing="GridViewRegion_RowEditing" OnRowCancelingEdit="GridViewRegion_RowCancelingEdit" OnRowDeleting="GridViewRegion_RowDeleting" OnRowUpdating="GridViewRegion_RowUpdating">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxRegionName" class="form-control" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="CountryId" SortExpression="CountryId">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxRegionCountryId" runat="server" Text='<%# Bind("CountryId") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("CountryId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <%--                <asp:BoundField DataField="CreateTime" HeaderText="CreateTime" InsertVisible="False" ReadOnly="True" SortExpression="CreateTime" />--%>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButtonRegionUpdate" class="btn btn-secondary" runat="server" CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButtonRegionUpdateCancel" class="btn btn-secondary" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonRegionEdit" class="btn btn-secondary" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonRegionDelete" class="btn btn-danger" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="javascript:return confirm('確定刪除？');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="PanelAgent" runat="server" Visible="false">
        <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="新增Agent" PostBackUrl="~/Admin/AddAgent.aspx" />
        <br />
        <br />
        <asp:DropDownList ID="DropDownListGridViewAgent" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListGridViewAgent_SelectedIndexChanged"></asp:DropDownList>
        <br />
        <table class="table align-items-center table-dark">
            <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">

                <ItemTemplate>
                    <tr>
                        <%--<td>
                            <asp:Label ID="Label15" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                        </td>--%>
                        <td>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# String.IsNullOrEmpty(Eval("CoverPath").ToString()) ?"" : Eval("CoverPath")  %>' />
                            <br />
                            <%--                            <asp:DropDownList ID="DropDownListChangeAgentImg" class="form-control" runat="server" Visible="false"></asp:DropDownList>--%>
                        </td>
                        <td>
                            <asp:Label ID="LabelName" runat="server" Text="Name" class="font-weight-bold text-yellow"></asp:Label>
                            <br />
                            <asp:Label ID="LabelAgentName" class="text-yellow" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                            <%--                            <asp:TextBox ID="TextBoxAgentName" class="form-control" runat="server" Text='<%#Bind("Name") %>' Visible="false" TextMode="MultiLine"></asp:TextBox>--%>
                            <br />
                            <asp:Label ID="LabelContact" runat="server" Text="Contact" class="font-weight-bold"></asp:Label>
                            <br />
                            <asp:Label ID="LabelAgentContact" runat="server" Text='<%#Eval("Contact") %>'></asp:Label>
                            <%--                            <asp:TextBox ID="TextBoxAgentContact" class="form-control" runat="server" Text='<%#Bind("Contact") %>' Visible="false"></asp:TextBox>--%>
                            <br />
                            <asp:Label ID="LabelAddress" runat="server" Text="Address" class="text-yellow font-weight-bold"></asp:Label>
                            <br />
                            <asp:Label ID="LabelAgentAddress" class="text-yellow" runat="server" Text='<%#Eval("Address") %>'></asp:Label>
                            <%--                            <asp:TextBox ID="TextBoxAgentAddress" class="form-control" runat="server" Text='<%#Bind("Address") %>' Visible="false" TextMode="MultiLine"></asp:TextBox>--%>
                            <br />
                            <asp:Label ID="LabelEmail" runat="server" Text="Email" class="font-weight-bold"></asp:Label>
                            <br />
                            <asp:Label ID="LabelAgentEmail" runat="server" Text='<%#Eval("Email") %>'></asp:Label>
                            <%--                            <asp:TextBox ID="TextBoxAgentEmail" class="form-control" runat="server" Text='<%#Bind("Email") %>' Visible="false" TextMode="MultiLine"></asp:TextBox>--%>
                            <br />
                            <asp:Label ID="LabelUrl" runat="server" Text="Url" class="text-yellow font-weight-bold"></asp:Label>
                            <br />
                            <asp:Label ID="LabelAgentUrl" class="text-yellow" runat="server" Text='<%#Eval("Url") %>'></asp:Label>
                            <%--                            <asp:TextBox ID="TextBoxAgentUrl" class="form-control" runat="server" Text='<%#Bind("Url") %>' Visible="false" TextMode="MultiLine"></asp:TextBox>--%>
                            <br />
                        </td>
                        <td>
                            <asp:Label ID="LabelTel" runat="server" Text="Tel" class="font-weight-bold"></asp:Label>
                            <br />
                            <asp:Label ID="LabelAgentTel" runat="server" Text='<%#Eval("Tel") %>'></asp:Label>
                            <%--                            <asp:TextBox ID="TextBoxAgentTel" class="form-control" runat="server" Text='<%#Bind("Tel") %>' Visible="false"></asp:TextBox>--%>
                            <br />
                            <asp:Label ID="LabelCell" runat="server" Text="Cell" class="text-yellow font-weight-bold"></asp:Label>
                            <br />
                            <asp:Label ID="LabelAgentCell" class="text-yellow" runat="server" Text='<%#Eval("Cell") %>'></asp:Label>
                            <%--                            <asp:TextBox ID="TextBoxAgentCell" class="form-control" runat="server" Text='<%#Bind("Cell") %>' Visible="false"></asp:TextBox>--%>
                            <br />
                            <asp:Label ID="LabelFax" runat="server" Text="Fax" class="font-weight-bold"></asp:Label>
                            <br />
                            <asp:Label ID="LabelAgentFax" runat="server" Text='<%#Eval("Fax") %>'></asp:Label>
                            <%--                            <asp:TextBox ID="TextBoxAgentFax" class="form-control" runat="server" Text='<%#Bind("Fax") %>' Visible="false"></asp:TextBox>--%>
                            <br />
                        </td>
                        <td>
                            <asp:Button ID="ButtonAgentEdit" class="btn btn-secondary" runat="server" Text="編輯" CommandArgument='<%#Eval("Id") %>' PostBackUrl='<%# "AgentContent.aspx?Id=" + Eval("Id") %>' />
                            <%--<asp:Button ID="ButtonAgentUpdate" class="btn btn-secondary" runat="server" Text="更新" CommandName="UpdateAgent" CommandArgument='<%#Eval("Id") %>' Visible="false" />
                            <asp:Button ID="ButtonAgentEditCancel" class="btn btn-secondary" runat="server" Text="取消" CommandName="CancelEditAgent" CommandArgument='<%#Eval("Id") %>' Visible="false" />--%>
                            <br />
                            <asp:Button ID="ButtonAgentDelete" class="btn btn-danger" runat="server" Text="刪除" OnClientClick="javascript:return confirm('確定刪除？');" CommandName="DeleteAgent" CommandArgument='<%#Eval("Id") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>



        <asp:GridView ID="GridViewAgent" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowEditing="GridViewAgent_RowEditing" OnRowCancelingEdit="GridViewAgent_RowCancelingEdit" OnRowDeleting="GridViewAgent_RowDeleting" OnRowUpdating="GridViewAgent_RowUpdating" OnRowDataBound="GridViewAgent_RowDataBound">
            <%--<EditRowStyle Width="1000px"/>--%>
            <Columns>
                <%--                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />--%>
                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxAgentName" runat="server" Text='<%# Bind("Name") %>' TextMode="MultiLine"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact" SortExpression="Contact">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxAgentContact" runat="server" Text='<%# Bind("Contact") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("Contact") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Address" SortExpression="Address">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxAgentAddress" runat="server" Text='<%# Bind("Address") %>' TextMode="MultiLine"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tel" SortExpression="Tel">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxAgentTel" runat="server" Text='<%# Bind("Tel") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Tel") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cell" SortExpression="Cell">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxAgentCell" runat="server" Text='<%# Bind("Cell") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label20" runat="server" Text='<%# Bind("Cell") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fax" SortExpression="Fax">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxAgentFax" runat="server" Text='<%# Bind("Fax") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Fax") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email" SortExpression="Email">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxAgentEmail" runat="server" Text='<%# Bind("Email") %>' TextMode="MultiLine"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Url" SortExpression="Url">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxAgentUrl" runat="server" Text='<%# Bind("Url") %>' TextMode="MultiLine"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Url") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonAgentDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="javascript:return confirm('確定刪除？');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButtonAgentUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButtonAgentUpdateCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonAgentEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯" PostBackUrl="AgentContent.aspx?"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
