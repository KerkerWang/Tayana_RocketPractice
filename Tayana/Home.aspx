<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Tayana.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
    <%--    <link href="css/js.css?v=0429" rel="stylesheet" type="text/css" />--%>
    <link href="css/style.css?v=042901" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript">


        $(function () {

            // 先取得 #abgne-block-20110111 , 必要參數及輪播間隔
            var $block = $('#abgne-block-20110111'),
                timrt, speed = 4000;


            // 幫 #abgne-block-20110111 .title ul li 加上 hover() 事件
            var $li = $('.title ul li', $block).hover(function () {
                // 當滑鼠移上時加上 .over 樣式
                $(this).addClass('over').siblings('.over').removeClass('over');
            }, function () {
                // 當滑鼠移出時移除 .over 樣式
                $(this).removeClass('over');
            }).click(function () {
                // 當滑鼠點擊時, 顯示相對應的 div.info
                // 並加上 .on 樣式

                $(this).addClass('on').siblings('.on').removeClass('on');
                var thisLi = $('#abgne-block-20110111 .bd .banner ul:eq(0)').children().eq($(this).index());
                $('#abgne-block-20110111 .bd .banner ul:eq(0)').children().hide().eq($(this).index()).fadeIn(1000);
                if (thisLi.children('input[type=hidden]').val() == 1) {
                    thisLi.children('.new').show();
                }

            });

            // 幫 $block 加上 hover() 事件
            $block.hover(function () {
                // 當滑鼠移上時停止計時器
                clearTimeout(timer);
            }, function () {
                // 當滑鼠移出時啟動計時器
                timer = setTimeout(move, speed);
            });

            // 控制輪播
            function move() {
                var _index = $('.title ul li.on', $block).index();
                _index = (_index + 1) % $li.length;
                $li.eq(_index).click();

                timer = setTimeout(move, speed);
            }

            // 啟動計時器
            timer = setTimeout(move, speed);

            //相簿輪撥初始值設定
            $('.title ul li:eq(0)').addClass('on');
            var thisLi = $('#abgne-block-20110111 .bd .banner ul:eq(0) li:eq(0)');
            thisLi.addClass('on');
            if (thisLi.children('input[type=hidden]').val() == 1) {
                thisLi.children('.new').show();
            }

            //最新消息TOP
            $('.newstop').each(function () {
                if ($(this).nextAll('input[type=hidden]').val() == 1) {
                    $(this).show();
                }
            });
        });


    </script>--%>
    <script type="text/javascript">

        $(function () {
            // 先取得 #abgne-block-20110111 , 必要參數及輪播間隔
            var $block = $('#abgne-block-20110111'),
                timrt, speed = 4000;

            // 幫 #abgne-block-20110111 .title ul li 加上 hover() 事件
            var $li = $('.title ul li', $block).hover(function () {
                // 當滑鼠移上時加上 .over 樣式
                $(this).addClass('over').siblings('.over').removeClass('over');
            }, function () {
                // 當滑鼠移出時移除 .over 樣式
                $(this).removeClass('over');
            }).click(function () {
                // 當滑鼠點擊時, 顯示相對應的 div.info
                // 並加上 .on 樣式
                var $this = $(this);
                $this.add($('.bd li.info', $block).eq($this.index())).addClass('on').siblings('.on').removeClass('on');
            });

            // 幫 $block 加上 hover() 事件
            $block.hover(function () {
                // 當滑鼠移上時停止計時器
                clearTimeout(timer);
            }, function () {
                // 當滑鼠移出時啟動計時器
                timer = setTimeout(move, speed);
            });

            // 控制輪播
            function move() {
                var _index = $('.title ul li.on', $block).index();
                _index = (_index + 1) % $li.length;
                $li.eq(_index).click();

                timer = setTimeout(move, speed);
            }

            // 啟動計時器
            timer = setTimeout(move, speed);
        });


        $(document).ready(function () {
            $('.slideshow').cycle({
                fx: 'fade' // choose your transition type, ex: fade, scrollUp, shuffle, etc...
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBackGroundImg" runat="server">
    <!--遮罩-->
    <div class="bannermasks">
        <img src="images/banner00_masks.png" alt="&quot;&quot;" />
    </div>
    <!--遮罩結束-->
    <!--------------------------------換圖開始---------------------------------------------------->
    <div id="abgne-block-20110111">
        <div class="bd">
            <div class="banner">
                <ul>
                    <asp:Repeater ID="RepeaterInfo" runat="server">
                        <ItemTemplate>
                            <li class="info"><a href="#">
                                <img src='<%#Eval("CoverPath") %>' /></a><!--文字開始--><div class="wordtitle">
                                    Tayana <span><%# Eval("Name").ToString().Substring(Eval("Name").ToString().IndexOf(" ") + 1,Eval("Name").ToString().Length-Eval("Name").ToString().IndexOf(" ")-1) %></span><br />
                                    <p>SPECIFICATION SHEET</p>
                                </div>
                                <!--文字結束-->
                                <!--新船型開始  54型才出現其於隱藏 -->
                                <asp:Panel ID="PanelNewDesign" runat="server" Visible='<%# Eval("Is_NewDesigned").ToString() == "True"? true: false %>'>
                                    <div class="new">
                                        <img src="images/new02.png" alt="new" />
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanelNewBuilding" runat="server" Visible='<%# Eval("Is_NewBuilt").ToString() == "True"? true: false %>'>
                                    <div class="new">
                                        <img src="images/new01.png" alt="new" />
                                    </div>
                                </asp:Panel>
                                <!--新船型結束-->
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%--<li class="info on"><a href="#">
                        <img src="images/banner001b.jpg" /></a><!--文字開始--><div class="wordtitle">
                            TAYANA <span>48</span><br />
                            <p>SPECIFICATION SHEET</p>
                        </div>
                        <!--文字結束-->
                    </li>
                    <li class="info"><a href="#">
                        <img src="images/banner002b.jpg" /></a><!--文字開始--><div class="wordtitle">
                            TAYANA <span>54</span><br />
                            <p>SPECIFICATION SHEET</p>
                        </div>
                        <!--文字結束-->
                        <!--新船型開始  54型才出現其於隱藏 -->
                        <div class="new">
                            <img src="images/new01.png" alt="new" />
                        </div>
                        <!--新船型結束-->
                    </li>
                    <li class="info"><a href="#">
                        <img src="images/banner003b.jpg" /></a><!--文字開始--><div class="wordtitle">
                            TAYANA <span>37</span><br />
                            <p>SPECIFICATION SHEET</p>
                        </div>
                        <!--文字結束-->
                    </li>
                    <li class="info"><a href="#">
                        <img src="images/banner004b.jpg" /></a><!--文字開始--><div class="wordtitle">
                            TAYANA <span>64</span><br />
                            <p>SPECIFICATION SHEET</p>
                        </div>
                        <!--文字結束-->
                    </li>
                    <li class="info"><a href="#">
                        <img src="images/banner005b.jpg" /></a><!--文字開始--><div class="wordtitle">
                            TAYANA <span>58</span><br />
                            <p>SPECIFICATION SHEET</p>
                        </div>
                        <!--文字結束-->
                    </li>
                    <li class="info"><a href="#">
                        <img src="images/banner006b.jpg" /></a><!--文字開始--><div class="wordtitle">
                            TAYANA <span>55</span><br />
                            <p>SPECIFICATION SHEET</p>
                        </div>
                        <!--文字結束-->
                    </li>--%>
                </ul>
                <!--小圖開始-->
                <div class="bannerimg title">
                    <ul>
                        <asp:Repeater ID="Repeaterbannerimg" runat="server" OnItemDataBound="Repeaterbannerimg_ItemDataBound">
                            <ItemTemplate>
                                <li class="" id="liItem">
                                    <div>
                                        <p class="bannerimg_p">
                                            <img src='<%#Eval("CoverPath") %>' alt="&quot;&quot;" style="object-fit: cover; width: 100%; height: 100%;" />
                                        </p>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <%--<li class="on">
                            <div>
                                <p class="bannerimg_p">
                                    <img src="images/i001.jpg" alt="&quot;&quot;" />
                                </p>
                            </div>
                        </li>
                        <li>
                            <div>
                                <p class="bannerimg_p">
                                    <img src="images/i002.jpg" alt="&quot;&quot;" />
                                </p>
                            </div>
                        </li>
                        <li>
                            <div>
                                <p class="bannerimg_p">
                                    <img src="images/i003.jpg" alt="&quot;&quot;" />
                                </p>
                            </div>
                        </li>
                        <li>
                            <div>
                                <p class="bannerimg_p">
                                    <img src="images/i004.jpg" alt="&quot;&quot;" />
                                </p>
                            </div>
                        </li>
                        <li>
                            <div>
                                <p class="bannerimg_p">
                                    <img src="images/i005.jpg" alt="&quot;&quot;" />
                                </p>
                            </div>
                        </li>
                        <li>
                            <div>
                                <p class="bannerimg_p">
                                    <img src="images/i006.jpg" alt="&quot;&quot;" />
                                </p>
                            </div>
                        </li>--%>
                    </ul>
                </div>
                <!--小圖結束-->
            </div>
        </div>
    </div>
    <!--------------------------------換圖結束---------------------------------------------------->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderOnlyForIndex" runat="server">
    <!--------------------------------最新消息---------------------------------------------------->
    <div class="news">
        <div class="newstitle">
            <p class="newstitlep1">
                <img src="images/news.gif" alt="news" />
            </p>
            <p class="newstitlep2"><a href="News.aspx">More>></a></p>
        </div>

        <ul>
            <asp:Repeater ID="RepeaterNews" runat="server">
                <ItemTemplate>
                    <li>
                        <asp:Panel ID="Panel1" runat="server" Visible='<%# Eval("Is_Top").ToString() == "True"? true : false %>'>
                            <div class="newstop">
                                <img src="images/new_top01.png" alt="&quot;&quot;" />
                            </div>
                        </asp:Panel>
                        <div class='<%# Eval("Is_Top").ToString() == "True"? "news01" : "news02" %>'>
                            <div class="news02p1">
                                <p class="news02p1img">
                                    <img src='<%#Eval("CoverPath") %>' alt="&quot;&quot;" />
                                </p>
                            </div>
                            <p class="news02p2">
                                <span><%# Eval("Title") %></span>
                                <a href='<%# "News.aspx?Id=" + Eval("Id") %>'><%# Eval("Content") %></a>
                            </p>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
            <!--TOP第一則最新消息-->
            <%--<li>
                <div class="newstop">
                    <img src="images/new_top01.png" alt="&quot;&quot;" />
                </div>
                <div class="news01">
                    <div class="news02p1">
                        <p class="news02p1img">
                            <img src="images/pit002.jpg" alt="&quot;&quot;" />
                        </p>
                    </div>
                    <p class="news02p2">
                        <span>Tayana 54 CE Certifica..</span>
                        <a href="#">For Tayana 54 entering the EU, CE Certificates are AVAILABLE to ensure conformity to all applicable European ...</a>
                    </p>
                </div>
            </li>--%>
            <!--TOP第一則最新消息結束-->

            <!--第二則-->
            <%--<li>
                <div class="news02">
                    <div class="news02p1">
                        <p class="news02p1img">
                            <img src="images/pit001.jpg" alt="&quot;&quot;" />
                        </p>
                    </div>
                    <p class="news02p2">
                        <span>Tayana 58 CE Certifica..</span>
                        <a href="#">For Tayana 58 entering the EU, CE Certificates are AVAILABLE to ensure conformity to all applicable European ...</a>
                    </p>
                </div>
            </li>
            <!--第二則結束-->

            <li>
                <div class="news02">
                    <div class="news02p1">
                        <p class="news02p1img">
                            <img src="images/pit001.jpg" alt="&quot;&quot;" />
                        </p>
                    </div>
                    <p class="news02p2">
                        <span>Big Cruiser in a Small ..</span>
                        <a href="#">Tayana 37 is our classical product and full of skilful craftsmanship. We only plan to build TWO units in a year.</a>
                    </p>
                </div>
            </li>--%>
        </ul>
    </div>
    <!--------------------------------最新消息結束---------------------------------------------------->

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderRoute" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
</asp:Content>
