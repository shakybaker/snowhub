<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetResortPopup.aspx.cs" Inherits="Sporthub.Web.Shared.Ajax.GetResortPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <%--<div class="headInfoWin"><h4><img alt="[<%= //vd.Country.ISO3166Alpha2 %>]" src="/assets/images/flags/<%= //vd.Country.ISO3166Alpha2 %>.png" />&nbsp;<%= //vd.Resort.Name %></h4></div>--%>
        <div class="headInfoWin"><h4><img alt="[FR]" src="/Static/Images/Flags/fr.png" />&nbsp;Chamonix-Mont-Blanc</h4></div>
        <p class="reviewsInfoWin"><a href="#" title="click to view member reviews">9 member reviews</a> | <a href="#" title="click to submit a review">Submit yours</a></p>
        <div id="example" class="tabContain">
            <ul class="tabnav">
                <li><a href="#fragment-1">Score</a></li>
                <li><a href="#fragment-2">Ability</a></li>
                <li><a href="#fragment-3">Nightlife</a></li>
                <li><a href="#fragment-4">Suits</a></li>
                <li><a href="#fragment-5">Expense</a></li>
            </ul>
            <div id="fragment-1" class="infoWinPanel">
                <table style="margin: 5px 0 0 10px;">
                <tr><td colspan="2"><img alt="1" src="/assets/images/star.png" /><img alt="1" src="/assets/images/star.png" /><img alt="1" src="/assets/images/star.png" /><img alt="0.5" src="/assets/images/star_half.png" /><img alt="0" src="/assets/images/star_empty.png" /> (based on <strong>120</strong> votes)</td></tr>
                <tr><td colspan="2">&nbsp;</td></tr>
                <tr>
                <td><img alt="[been]" src="/assets/images/tick_shield.png" /></td>
                <td><strong>178</strong> members have been to Chamonix</td>
                </tr>
                <tr>
                <td><img alt="[favourite]" src="/assets/images/heart.png" /></td>
                <td><strong>30</strong> members call Chamonix a favourite</td>
                </tr>
                </table>
            </div>
            <div id="fragment-2" class="infoWinPanel">
                <ul class="rating">
                <li><span>Expert</span> <img id="img1" alt="8%" src="/assets/images/bar_orange.gif" width="16" height="8" /></li>
                <li><span>Advanced</span> <img id="img2" alt="18%" src="/assets/images/bar_orange.gif" width="36" height="8" /></li>
                <li><span>Intermediate</span> <img id="img3" alt="40%" src="/assets/images/bar_orange.gif" width="80" height="8" /></li>
                <li><span>Beginner</span> <img id="img4" alt="24%" src="/assets/images/bar_orange.gif" width="48" height="8" /></li>
                </ul>
            </div>
            <div id="fragment-3" class="infoWinPanel">
                <ul class="rating">
                <li><span>Lively</span> <img id="img5" alt="8%" src="/assets/images/bar_green.gif" width="6" height="8" /></li>
                <li><span>Average</span> <img id="img6" alt="18%" src="/assets/images/bar_green.gif" width="36" height="8" /></li>
                <li><span>Quiet</span> <img id="img7" alt="40%" src="/assets/images/bar_green.gif" width="80" height="8" /></li>

                </ul>
            </div>
            <div id="fragment-4" class="infoWinPanel">
                <ul class="rating">
                <li><span>Skiers</span> <img id="img8" alt="8%" src="/assets/images/bar_purple.gif" width="32" height="8" /></li>
                <li><span>Snowboarders</span> <img id="img9" alt="18%" src="/assets/images/bar_purple.gif" width="72" height="8" /></li>

                </ul>
            </div>
            <div id="fragment-5" class="infoWinPanel">
                <ul class="rating">
                <li><span>Expensive</span> <img id="img10" alt="8%" src="/assets/images/bar_blue.gif" width="32" height="8" /></li>
                <li><span>Affordable</span> <img id="img11" alt="18%" src="/assets/images/bar_blue.gif" width="72" height="8" /></li>

                <li><span>Cheap</span> <img id="img12" alt="18%" src="/assets/images/bar_blue.gif" width="72" height="8" /></li>
                </ul>
            </div>
        </div>

    </form>
</body>
</html>
