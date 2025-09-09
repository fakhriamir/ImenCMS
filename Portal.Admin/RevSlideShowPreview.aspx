<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="RevSlideShowPreview.aspx.cs" Inherits="Portal.Admin.RevSlideShowPreview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
     <style>

        #controll {
            border-right: dotted 1px #808080;
            float: right;
            padding: 0px 3px;
            color: white;
            cursor: pointer;
            z-index: 2020;
        }

        #robicon1 {
            padding: 0;
            margin: 0 auto;
            height: 10px;
            float: right;
        }

            #robicon1 ul {
                padding: 0;
                margin: 0;
            }

            #robicon1 li {
                color: #ffffff;
                background-image: url('/Images/bc0.png');
                text-align: center;
                float: right;
                list-style: none outside none;
                cursor: pointer;
                width: 16px;
                height: 24px;
            }

        #nav {
            padding: 0;
            margin: 0 auto;
            height: <%=Tools.Tools.GetSetting(429,"55")%>px;
        }

            #nav ul {
                padding: 0;
                margin: 0;
            }

            #nav li {
                color: <%=Tools.Tools.GetSetting(430)%>;
                margin: 0;
                background: <%=Tools.Tools.GetSetting(431)%>;
                text-align: center;
                float: right;
                font-size: 15pt;
                list-style: none outside none;
                cursor: pointer;
                padding-top: 8px;
                text-decoration: none;
                text-shadow: 0.5px 0.5px #000000;
                position: relative;
                display: none;
            }

        .samenearob-div {
            font-family: 'B Koodak';
            height: 300px;
            width: 900px;
            z-index: 1010;
            overflow: hidden;
            padding: 0;
            margin: 0;
            position: relative;
        }

            .samenearob-div div {
                position: absolute;
            }

            .samenearob-div a {
                text-decoration: none;
                border: medium none;
            }

            .samenearob-div img {
                text-decoration: none;
                border-style: none;
                border: white;
                border: none;
            }

            .samenearob-div li {
                list-style: none;
                overflow: hidden;
                padding: 0;
                margin: 0;
                position: relative;
                width: 900px;
                height: 300px;

            }

            .samenearob-div ul {
                padding: 0;
                padding-left: 12px;
                margin: 0;
            }

            .samenearob-div img {
            }

        #nav .showR {
            background-color: #67cf90;
        }

        #robicon1 .showB {
            background-image: url('/Images/bc1.png');
        }


        #nav div {
            position: relative;
            bottom: 70px;
            background: #808080;
            border: 1px groove #808080;
            border-radius: 10px 5px;
            z-index: 2010;
            padding: 7px;
            text-align: center;
            margin: 0 auto;
            display: none;
        }

        #nav li:hover > div {
            display: block;
        }

        #samenea_gallery {
            width: <%=Tools.Tools.GetSetting(427)%>px;
            height: <%=Tools.Tools.GetSetting(428)%>px;
            margin: 0 auto;
            padding: 0;
        }

        #left {
            height: 24px;
            /*position: relative;*/
            bottom: 350px;
            float: right;
            z-index: 2010;
        }

        .stop {
            background-image: url('/Images/bcpause.png');
            background-position: center;
            width: 14px;
            height: 24px;
        }

        .play {
            background-image: url('/Images/bcplay.png');
            background-position: center;
            width: 14px;
            height: 24px;
        }
    </style>

<div id="samenea_gallery">

    <div class="samenearob-div">
        <ul>
           <%=completeList %>
        </ul>
    </div>

    <div id="nav"  time2='<%=Tools.Tools.GetAdminSetting(426)%>'>
        <ul>
            <asp:Repeater ID="RepCaption" runat="server">
                <ItemTemplate>
                    <li>
                       <%# Eval("Title")%>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>

    <div id="left">
        <div id="robicon1"></div>
        <div id="controll"></div>
    </div>

 
</div>

<script type="text/javascript" language="javascript" src="/Scripts/SamenSlideShow.js"></script>

</asp:Content>
