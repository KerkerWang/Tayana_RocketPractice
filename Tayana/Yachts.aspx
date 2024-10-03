<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Yachts.aspx.cs" Inherits="Tayana.Yachts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
    <link rel="stylesheet" type="text/css" href="css/jquery.ad-gallery.css?v=04245">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.ad-gallery.js"></script>
    <script type="text/javascript">
        $(function () {

            var galleries = $('.ad-gallery').adGallery();
            galleries[0].settings.effect = 'slide-hori';



        });
    </script>
    <link href="css/homestyle01.css?v=0429" rel="stylesheet" type="text/css" />
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBackGroundImg" runat="server">
    <!--遮罩-->
    <div class="bannermasks">
        <img src="images/banner01_masks.png" alt="&quot;&quot;" />
    </div>
    <!--遮罩結束-->

    <div class="banner">
        <div id="gallery" class="ad-gallery">
            <div class="ad-image-wrapper">
            </div>
            <div class="ad-controls" style="display: none">
            </div>
            <div class="ad-nav">
                <div class="ad-thumbs">
                    <ul class="ad-thumb-list">
                        <asp:Repeater ID="RepeaterGallery" runat="server">
                            <ItemTemplate>
                                <li>
                                    <a href='<%#Eval("PhotoPath") %>'>
                                        <img src='<%#Eval("PhotoPath") %>'>
                                    </a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </div>
    </div>



    <!--------------------------------換圖結束---------------------------------------------------->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderOnlyForIndex" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
    <div class="left">
        <div class="left1">
            <p><span>YACHTS</span></p>
            <ul>
                <asp:Repeater runat="server" ID="RepeaterLeft">
                    <ItemTemplate>
                        <li><a href='Yachts.aspx?Id=<%#Eval("Id") %>'><%#Eval("Name") %><%#bool.Parse(Eval("Is_NewDesigned").ToString()) ? " (New Design)":bool.Parse(Eval("Is_NewBuilt").ToString()) ?" (New Build)":""%></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderRoute" runat="server">
    <a href="#">Home</a> >> <a href="#">Yachts</a> >> <a href='Yachts.aspx?Id=<%= String.IsNullOrEmpty(Request.QueryString["Id"]) ? "1" : Request.QueryString["Id"] %>'><span class="on1">
        <asp:Literal ID="LiteralRoute" runat="server"></asp:Literal>
    </span></a>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    <asp:Literal ID="LiteralTitle" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <!--次選單-->
    <div class="menu_y">
        <ul>
            <li class="menu_y00">YACHTS</li>
            <li><a class="menu_yli01" href='Yachts.aspx?Id=<%= String.IsNullOrEmpty(Request.QueryString["Id"]) ? "1" : Request.QueryString["Id"] %>&Overview=true'>Overview</a></li>
            <li><a class="menu_yli02" href='Yachts.aspx?Id=<%= String.IsNullOrEmpty(Request.QueryString["Id"]) ? "1" : Request.QueryString["Id"] %>&Layout=true'>Layout & deck plan</a></li>
            <li><a class="menu_yli03" href='Yachts.aspx?Id=<%= String.IsNullOrEmpty(Request.QueryString["Id"]) ? "1" : Request.QueryString["Id"] %>&Specification=true'>Specification</a></li>
        </ul>
    </div>
    <!--次選單-->
    <asp:Panel ID="PanelOverview" runat="server">
        <div class="box1">
            <asp:Literal ID="LiteraOverviewContent" runat="server"></asp:Literal>
            <br />

        </div>
        <div class="box3">
            <h4>
                <asp:Literal ID="LiteraDimensionTitle" runat="server"></asp:Literal>
                DIMENSION</h4>
            <table class="table02">
                <tr>
                    <td class="table02td01">
                        <table>
                            <asp:Repeater ID="RepeaterDimension" runat="server">
                                <ItemTemplate>
                                    <tr class='<%# Convert.ToInt32(Eval("Rank"))%2==1? "":"tr003" %>'>
                                        <th>
                                            <%#Eval("DimensionKey") %></th>
                                        <td>
                                            <%#Eval("DimensionValue") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </td>
                    <td>
                        <asp:Image ID="ImageDimension" runat="server" Width="278" Height="345" AlternateText="&quot;&quot;" />
                    </td>
                </tr>
            </table>
        </div>
        <p class="topbuttom">
            <img src="images/top.gif" alt="top" />
        </p>
        <!--下載開始-->
        <div class="downloads">
            <p>
                <img src="images/downloads.gif" alt="&quot;&quot;" />
            </p>
            <ul>
                <asp:Repeater ID="RepeaterDownload" runat="server">
                    <ItemTemplate>
                        <li><a href='<%#Eval("DownloadFilePath") %>'><%#Eval("DownloadFileName") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <!--下載結束-->
    </asp:Panel>
    <asp:Panel ID="PanelLayout" runat="server" Visible="false">
        <div class="box6">
            <p>Layout & deck plan</p>
            <ul>
                <asp:Repeater ID="RepeaterLayout" runat="server">
                    <ItemTemplate>
                        <li>
                            <img src='<%#Eval("PhotoPath") %>' alt="&quot;&quot;" /></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </asp:Panel>
    <asp:Panel ID="PanelSpecification" runat="server">
        <div class="box5">
            <h4>DETAIL SPECIFICATION</h4>
            <asp:Repeater ID="RepeaterSpecification" runat="server" OnItemDataBound="RepeaterSpecification_ItemDataBound">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Id") %>' Visible="false"></asp:Label>
                    <p>
                        <%#Eval("Name") %>
                    </p>
                    <asp:Literal ID="LiteralSpecificationTitle" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <p class="topbuttom">
            <img src="images/top.gif" alt="top" />
        </p>
    </asp:Panel>
</asp:Content>
