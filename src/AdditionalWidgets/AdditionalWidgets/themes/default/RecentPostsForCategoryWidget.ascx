<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AdditionalWidgets.AdditionalWidgetsModels.RecentPostsForCategoryWidgetModel>" %>
<% if (Model != null) { %>
<div class="feedRecentList">
    <h3><%= Model.Category %></h3>
    <% if (Model.Entries.Any()) { %>
      <ul>
	     <% foreach (AtomEntry entry in Model.Entries) { %>
		    <li><a href="<%= Url.RouteIdUrl("BlogEntry", entry.Id) %>"><%= entry.Title.ToString() %></a>
		    <em>updated <%= Html.DateTimeAgoAbbreviation(entry.Updated) %></em></li>
	     <% } %>
      </ul>
    <% } else { %>
        <div style="color:Red;">There is nothing to display.</div><br />
    <% } %>
</div>
<%} %>