<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IList<TwitterPluginForAtomSite.TwitterStructs.Tweet>>" %>
<% foreach (var p in Model)
   { %>
<div class="TwitterStatus">
    <div class="TwitterStatusText">
        <%= p.Text %></div>
    <div class="TwitterStatusFooter">
        <span class="TwitterStatusTime">
            <%= Html.DateTimeAgoAbbreviation(p.CreatedAt) %>
        </span>from <span class="TwitterStatusSource">
            <%= p.Source %>
        </span>
        <% if (!string.IsNullOrEmpty(p.InReplyToScreenName))
           { %>
        <span class="TwitterInResponseTo"><a href="<%= p.InReplyToStatusURL %>">in reply to
            <%= p.InReplyToScreenName%></a>.. </span>
        <% } %>
    </div>
</div>
<% } %>