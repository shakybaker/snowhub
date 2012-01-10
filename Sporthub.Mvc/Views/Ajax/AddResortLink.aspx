<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Sporthub.Mvc.ViewData.AddResortLinkViewData>" %>

<form id="AddResortLinkForm" action="<%= Url.Action("AddResortLink", "Ajax") %>" method="post">

<% if (!string.IsNullOrEmpty(Model.Message))
   { %>
   <p><%= Model.Message %></p>
   <%}
   else
   { %>
    <label for="Name">Link Name</label>
    <%= Html.TextBox("Name", Model.ResortLink.Name, new { @class = "tb tb250" })%>
    <label for="URL">Link Url</label>
    <%= Html.TextBox("URL", Model.ResortLink.URL, new { @class = "tb tb250" })%>
    <label for="Sequence">Sequence</label>
    <%= Html.TextBox("Sequence", Model.ResortLink.Sequence, new { @class = "tb tb50" })%>

    <span class="button"><button type="submit" name="submit" value="Submit">Submit</button></span> 
    <a href="#" class="closeThickBox">Cancel</a>
    <input id="ResortID" name="ResortID" type="hidden" value="<%= Model.ResortLink.ResortID %>" />
<%} %>
</form>

<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
<script type="text/javascript" src="/Static/Scripts/sporthub.js"></script>
<script type="text/javascript" language="javascript">

    $(document).ready(function() {
        $('form#AddResortLinkForm').submit(function() {
            sporthub.addResortLink.submitForm($(this));
			return false;
		});
    });
</script>
