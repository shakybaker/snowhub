<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Sporthub.Model.WeatherForecast>" %>

<% if (Model.ForecastDays.Count > 0) { %>
<table>
<% foreach (Sporthub.Model.ForecastDay day in Model.ForecastDays) { %>
    <tr>
    <td style="vertical-align: top; text-align: center; width: 70px;">
    <img style="opacity: 0.6" src="<%= day.IconUrl %>" alt="<%= day.IconAlt %>" />
    </td>
    <td style="vertical-align: top">
    <span style="font-size: 0.9em; font-weight: bold; display: block; margin: 7px 0 0 0;"><%= day.DayOfWeek %></span>
    <span style="font-size: 0.9em; font-weight: normal; display: block; width: 140px; margin: 2px 0 0 0;"><%= day.Conditions %></span>
    <span style="font-size: 0.8em; font-weight: normal; display: block; width: 140px; margin: 3px 0 0 0;"><%= day.LoTempC%>&deg;C (<%= day.LoTempF%>&deg;F) to <%= day.HiTempC%>&deg;C (<%= day.HiTempF%>&deg;F)</span><br />
    </td>
    </tr>
<% } %>
</table>
<% } else { %>
    <p>Error fetching weather feed</p>
<% } %>
