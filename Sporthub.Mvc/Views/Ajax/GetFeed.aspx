<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.NewsFeedItemsViewData>" %>

<%
    if (Model.Items.Count > 0)
    {
%>
<ul class="list1">
<%
        foreach (Sporthub.Model.NewsFeedItem item in Model.Items)
        {
%>
    <li>
        <a href="<%=item.Url%>" title="opens in a new window" target="_blank"><%=item.Title%></a>
    </li>
<%
        }
%>
</ul>
<%
    }
    else
    {
%>
    <p style="width: 200px;"><%=Model.Message%></p>
<%
    }
%>

<script type="text/javascript" language="javascript">

    $(document).ready(function() {

    });
    
</script>
