<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Masters/Sporthub.Master" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.SiteViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="container_12">
        <div class="grid_12">
            <div class="pod guidelines" style="margin-bottom: 140px">
<h3>theSnowhub.com Posting Guidelines</h3>

<p>Note: We offer users the ability to post resort reviews and also engage in discussions in our forums.  We will delete posts that we find detract from the enjoyment of reading theSnowhub.com. We ask that posters abstain from the following list of 15 types of posts. Those that consistently ignore these guidelines will be blocked from posting further comments. If you are interested in posting outside of these guidelines, there are numerous other places where you can post such messages.</p>
<p>The following types of posts may be deleted:</p>
<p>1. Use of offensive<strong> racial</strong> / <strong>gender</strong> / <strong>sexual preference</strong> / <strong>religion-bashing terms</strong> or other hurtful speech. If you use a word with racial overtones in your post name, that counts. Even if you find it a source of pride, the Web doesn&#8217;t know what nationality you are. All it sees is racial slurs on theSnowhub.com.</p>

<p>2. <strong>Excessive use of swear words</strong>, and swearing in titles of comments / messages / reviews.</p>
<p>3. <strong>Spam</strong> and links to spam. If you want to advertise, contact us directly.</p>
<p>4. &#8220;<strong>First Posts</strong>,&#8221; &#8220;nth post,&#8221; and discussion of them with no meaningful commentary. We don&#8217;t mind if you want to be first and list &#8220;first post&#8221; as your subject, just add some meaningful commentary besides a one-liner that is obviously there to satisfy this requirement.</p>
<p>5. <strong>Links to inappropriate or unrelated content</strong> such as porn, distasteful pictures, etc. Many people read theSnowhub.com from workplaces. Linking to this type of content changes theSnowhub.com to a site that could get users in trouble at their place of employment.</p>
<p>6. Posting of any <strong>personal information</strong>, names, addresses, phone numbers, e-mails, etc. If you want to include your e-mail in a post, that&#8217;s fine. Just don&#8217;t type someone else&#8217;s information and then make comments about that person.</p>
<p>7. <strong>Nonsensical posts</strong> that have nothing to do with the topic, <strong>repetitive diatribes</strong> posted to multiple items, off-topic posts that hinder discussion, and responses to those posts. We ask readers to ignore such posts. It&#8217;s hard to keep the comments clear of them if several people respond to them and responses mix with actual commentary.</p>

<p>8. <strong>Personal attacks</strong> on other posters or theSnowhub.com writers. Comment on ideas, not people. theSnowhub.com writers are people with feelings, as are other commenters. There is no need to abuse them.</p>
<p>9. <strong>Product activation codes</strong> for software, requests for such, or other <strong>warez-related posts</strong> or links</p>

<p>10. <strong>Useless flame bait</strong>: &#8220;XYZ Sucks &#8211; flame away!&#8221; In other words: useless, blatantly inflammatory messages not to be confused with useful flame bait, where someone has an opinion that greatly differs from the norm and wishes to express it in a meaningful way.</p>
<p>11. <strong>Impersonations</strong> of other users&#8217; post names and related discussion about how the post is not from that user.</p>
<p>12. <strong>Double postings and &#8220;Sorry for the double post&#8221; messages</strong>. We will delete the double and the &#8220;sorry.&#8221; If it happens to you, don&#8217;t worry about it.</p>

<p>13. Anti-Canada, anti-England, or other<strong> nation-bashing and associated comments directed at the people of a nation</strong> and not the policies. We are a site about snow sports, not geography. Also, if someone has trouble with English, please be respectful, as it&#8217;s most probably not their first language.</p>
<p>14. <strong>Posts announcing other unrelated news</strong>. Please submit breaking news directly to us instead of killing a discussion. We will get to it.</p>
<p>15. <strong>Incomprehensible posts or posts in ALL CAPS</strong>. All caps posts are the equivalent of shouting.</p>
<br />
<br />
<h3>Contacting theSnowhub.com</h3>
<p>If you have any questions or comments about these posting guidelines or feel that your posts have been unnecessarily deleted, you can <a href="/contactus/">contact us</a>. We will read your comments and consider them.</p>
                <div class="cb"></div>
                <div class="podbtm"></div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBrowserTitle" runat="server">Posting Guidelines</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PageHeading" runat="server">
    <div class="grid_12">
        <h2>Posting Guidelines</h2>
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
