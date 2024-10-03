<%@ Page Title="編輯Agent頁面" Language="C#" MasterPageFile="~/Admin/Site2.Master" AutoEventWireup="true" CodeBehind="AgentContent.aspx.cs" Inherits="Tayana.Admin.AgentContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdd" runat="server">
    <asp:Button ID="Button1" class="btn btn-secondary" runat="server" Text="返回Dealers頁面管理" PostBackUrl="~/Admin/Dealers.aspx" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row mx-0">
        <asp:Image ID="ImageAgent" class="col-3" runat="server" />
        <div class="col-5">
            <div class="form-group">
                <div class="input-group row mx-0">
                    <div class="input-group-prepend">
                        <span class="input-group-text">選擇相片</span>
                    </div>
                    <asp:FileUpload ID="FileUpload1" class="form-control" runat="server" />
                    <asp:Button ID="ButtonUpload" class="btn btn-primary" runat="server" Text="上傳" OnClick="ButtonUpload_Click" />
                </div>
            </div>
            <div class="form-group">
                <div class="input-group row mx-0">
                    <div class="input-group-prepend">
                        <span class="input-group-text">選擇相片</span>
                    </div>
                    <asp:DropDownList ID="DropDownListAgentImg" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListAgentImg_SelectedIndexChanged"></asp:DropDownList>
                    <asp:Button ID="ButtonChangeAgentImg" class="btn btn-primary" runat="server" Text="更換" OnClick="ButtonChangeAgentImg_Click" />
                </div>
            </div>
        </div>
    </div>
    <br />
    <br />
    <div class="form-group">
        <div class="input-group row mx-0">
            <div class="input-group-prepend">
                <span class="input-group-text">代理商名稱</span>
            </div>
            <asp:TextBox ID="TextBoxAgentName" class="form-control col-3" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="input-group row mx-0">
            <div class="input-group-prepend">
                <span class="input-group-text">代理商聯絡人</span>
            </div>
            <asp:TextBox ID="TextBoxAgentContact" class="form-control col-3" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="input-group row mx-0">
            <div class="input-group-prepend">
                <span class="input-group-text">代理商地址</span>
            </div>
            <asp:TextBox ID="TextBoxAgentAddress" class="form-control col-3" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="input-group row mx-0">
            <div class="input-group-prepend">
                <span class="input-group-text">代理商電話號碼</span>
            </div>
            <asp:TextBox ID="TextBoxAgentTel" class="form-control col-3" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="input-group row mx-0">
            <div class="input-group-prepend">
                <span class="input-group-text">代理商手機號碼</span>
            </div>
            <asp:TextBox ID="TextBoxAgentCell" class="form-control col-3" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="input-group row mx-0">
            <div class="input-group-prepend">
                <span class="input-group-text">代理商傳真號碼</span>
            </div>
            <asp:TextBox ID="TextBoxAgentFax" class="form-control col-3" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="input-group row mx-0">
            <div class="input-group-prepend">
                <span class="input-group-text">代理商電子信箱</span>
            </div>
            <asp:TextBox ID="TextBoxAgentEmail" class="form-control col-3" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="input-group row mx-0">
            <div class="input-group-prepend">
                <span class="input-group-text">代理商網址</span>
            </div>
            <asp:TextBox ID="TextBoxAgentUrl" class="form-control col-3" runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="ButtonUpdateAgent" class="btn btn-primary" runat="server" Text="修改" OnClick="ButtonUpdateAgent_Click"/>
        </div>
    </div>
</asp:Content>
