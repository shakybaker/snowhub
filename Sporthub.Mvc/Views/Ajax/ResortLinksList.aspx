<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.ResortLinksListViewData>" %>

<%
    if (Model.ResortLinks.Count > 0)
    {
%>
<table class="Stats w400">
    <thead><tr><th class="w10">Seq</th><th class="w15">Type</th><th class="w65">Description &amp; Link</th><th class="w30">&nbsp;</th></tr></thead>
    <tbody>
<%
        foreach (Sporthub.Model.ResortLink resortLink in Model.ResortLinks)
        {
%>
    <tr class="editableList" id="resortLink_<%=resortLink.ID %>">
        <td><%=resortLink.Sequence%></td>
        <td><%=resortLink.ResortLinkTypeID%></td>
        <td><%=resortLink.Name%><a rel="external" href="<%=resortLink.URL %>"><br /><%=resortLink.URL%></a></td>
        <td style="width: 35px"><a class="editBtn edit" href="#"><img alt="edit" src="/static/images/buttons/pencil.png" /></a>&nbsp;<a class="editBtn delete" href="#"><img alt="delete" src="/static/images/buttons/cross.png" /></a></td>
    </tr>
<%
        }
%>
    </tbody>
<%
    }
    else
    {
%>
    <p>No Links added yet</p>
<%
    }
%>
</table>

<script type="text/javascript" language="javascript">

    $(document).ready(function() {

        $("tr.editableList").hover(
            function() {
                $(this).find("a.editBtn").css("visibility", "visible");
            },
            function() {
                $(this).find("a.editBtn").css("visibility", "hidden");
            }
        );

    });
    
</script>
