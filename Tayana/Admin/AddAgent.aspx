<%@ Page Title="新增Agent頁面" Language="C#" MasterPageFile="~/Admin/Site2.Master" AutoEventWireup="true" CodeBehind="AddAgent.aspx.cs" Inherits="Tayana.Admin.AddAgent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdd" runat="server">
    <asp:Button ID="Button1" class="btn btn-secondary" runat="server" Text="返回Dealers頁面管理" PostBackUrl="~/Admin/Dealers.aspx"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-group col-2 pr-0">
        <div class="input-group row mx-0">
            <div class="input-group-prepend">
                <span class="input-group-text">地區</span>
            </div>
            <asp:DropDownList ID="DropDownListRegion" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListRegion_SelectedIndexChanged"></asp:DropDownList>
        </div>
    </div>
    <div class="form-group">
        <div class="input-group row mx-0">
            <div class="input-group-prepend">
                <span class="input-group-text">代理商圖片</span>
            </div>
            <asp:FileUpload ID="FileUpload1" class="form-control col-3" runat="server" />
        </div>
    </div>
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
            <asp:TextBox ID="TextBoxAgentAddress" class="form-control col-3" runat="server"></asp:TextBox>
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
            <asp:TextBox ID="TextBoxAgentUrl" class="form-control col-3" runat="server"></asp:TextBox>
            <asp:Button ID="ButtonAddAgent" class="btn btn-primary" runat="server" Text="新增" OnClientClick="javascript:return confirm('確定新增？');" OnClick="ButtonAddAgent_Click" />
        </div>
    </div>
</asp:Content>
