<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.SiteViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMain" runat="server">
<form class="thisForm" action="<%= Url.Action("AboutUs", "Site") %>" method="post">
    <div class="container_12">
        <div class="grid_4">
            <div class="pod">
                <div class="headwrap">
                    <h3>The Site</h3>
                </div>
                <div class="podIn">
                    <p>The Snowhub has been created by a small group of snow-sports enthusiasts who are commited to making it 'the' place to go online for information and discussion for all things snow-sports related.</p>
                    
                    <p>Our plan is to grow the site gradually. We have launched the site with fairly minimal features but we have many plans in the pipeline and will add features over time and (hopefully) as the site gains popularity.</p>
                    
                    <p>The main focus for now is to provide a platform for users to effectively rate and review resorts. </p>
                </div>
            </div>
        </div>
        <div class="grid_4">
            <div class="pod">
                <div class="headwrap">
                    <h3>The Team</h3>
                </div>
                <div class="podIn">
                    <p><a class="profile" href="/user/shaky"><img alt="profile pic" src="/content/users/shaky_lge.png" class="tnMedia" /></a>
                                Mark is a snowboarding veteran of 15 years, has visited more than 50 ski resorts and spent 3 full seasons in the mountains. His first trip was to <a href="/resorts/les-2-alpes">Les Deux Alpes</a> with a group of friends from work; he loved it so much that when he got home he quit his job and went to Colorado for the rest of the season.</p>

                    <p>Mark has been to many fantastic resorts but his all-time favourite, because of its stunning beauty and endless off-piste, is <a href="/resorts/chamonix">Chamonix in the French Alps</a>.</p>

                    <p>After years of organising and researching his own trips he felt that although snow sports were well-served on the internet there was a need for a 'one-stop' website which would be well-designed, information-packed, community-based and supported by relevant advertising. Seeing as Mark works in Web Development he felt obliged to be the person to create it!</p>
<hr />
                    <p><a class="profile" href="/user/walt"><img alt="profile pic" src="http://graph.facebook.com/100000468646138/picture" class="tnMedia"/></a>Dave has been a skier since school trips back in the eighties but really found his passion for snow sports when he gave snowboarding a go on a trip to <a href="/resorts/st-anton">St. Anton, Austria</a>. Since then he has spent a lot of time in various European resorts but his favourite trip was 3 weeks in <a href="/resorts/chile/list">Chile</a> one summer.</p>


<hr />
                    <p><a class="profile" href="/user/eevee"><img alt="profile pic" src="/content/users/eevee_lge.png" class="tnMedia" /></a>Yvette grew up with skiing and has skied expertly since 5 years old. <a href="/resorts/austria/list">Austrian</a> resorts are her favourites, the best being <a href="/resorts/kitzbühel">Kitzbühel</a>. She is so obsessed that you can often find her on the <a href="/resorts/hintertux">Hintertux</a> glacier of a summer weekend. </p>
                </div>
            </div>
        </div>
        <div class="grid_4">
            <div class="pod">
                <div class="headwrap">
                    <h3>Contact Us</h3>
                </div>
                <div class="podIn">
                    <label>Email Address</label>
                    <div class="cb"></div>
                    <input maxlength="100" id="email" name="email" type="text" class="tb" style="width: 280px" value="" />
                    <div class="cb"></div>
                    <label>Message</label>
                    <div class="cb"></div>
                    <textarea cols="28" style="height: 180px; margin: 0 0 30px" id="message" name="message"></textarea>
                    <div class="cb"></div>
                    <span style="margin: 10px 10px 0 0;" class="button"><button type="submit" name="submit" value="Send">Send</button></span> 
                </div>
            </div>
        </div>
    </div>
</form>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Snowhub : About Us</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageHeading" runat="server">
    <div class="grid_12">
        <h2>About Us</h2>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHead" runat="server">
<meta name="robots" content="index, follow, noodp" />
<meta name="description" content="The Snowhub is an online snow sports community. Join for free and rate resorts, take part in the forums, upload pictures, plan trips and much more." />
<meta name="keywords" content="ski skiing snowboard snowboarding snow sport mountain forums pictures nightlife reviews ratings maps holiday travel vacation hotels accomodation" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentBreadcrumb" runat="server">
<ul>
<%
    int i = 1;
    foreach (Sporthub.Model.Breadcrumb bc in Model.Breadcrumbs)
    {
%>
    <li class='bcMenuitem' id='bcMenuitem_<%=(i== Model.Breadcrumbs.Count) ? 0 : i %>'><a href='<%= bc.Url %>'><%if (i < Model.Breadcrumbs.Count){%><span class='bcArrow'><%}%><%= bc.Name %><%if (i< Model.Breadcrumbs.Count){%></span><%}%></a></li>
<%
        i++;
    }
%>                
</ul>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentScripts" runat="server">
</asp:Content>
