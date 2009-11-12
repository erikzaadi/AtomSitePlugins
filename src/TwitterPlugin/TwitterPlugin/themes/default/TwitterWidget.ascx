<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TwitterPluginForAtomSite.Models.ClientModel>" %>
<% 
    if (Model.TwitterResponse != null)
    {%>
<div class="TwitterWidget">
    <h3>
        Twitter</h3>
    <div class="TwitterTitle TwitterRounded">
            <a href="<%= Model.TwitterResponse.User.ProfileURL %>" title="<%= Model.TwitterResponse.User.Description %>">
                <img src="<%= Model.TwitterResponse.User.ImageURL %>" alt="Twitter Image" /></a><span
                    class="TwitterTitleName"><%= Model.TwitterResponse.User.Name%></span>
    </div>
    <div class="TwitterStatuses TwitterRounded">
        <% foreach (var p in Model.TwitterResponse.Tweets)
           { %>
        <div class="TwitterStatus TwitterRounded">
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
    </div>
    <div class="TwitterBottomInfo TwitterRounded">
        <div class="TwitterBottomInfoItem">
            <a href="<%= Model.TwitterResponse.User.TwitterHomeURL %>"><span class="TwitterBottomNumber">
                <%=  Model.TwitterResponse.User.StatusCount%>
            </span><span class="TwitterBottomLabel">Tweets</span> </a>
        </div>
        <div class="TwitterBottomInfoItem">
            <a href="<%= Model.TwitterResponse.User.FriendsLink %>"><span class="TwitterBottomNumber">
                <%=  Model.TwitterResponse.User.FriendsCount %>
            </span><span class="TwitterBottomLabel">Following</span> </a>
        </div>
        <div class="TwitterBottomInfoItem">
            <a href="<%= Model.TwitterResponse.User.FollowersLink %>"><span class="TwitterBottomNumber">
                <%=  Model.TwitterResponse.User.Followers %>
            </span><span class="TwitterBottomLabel">Followers</span> </a>
        </div>
    </div>
</div>
<%} %>
