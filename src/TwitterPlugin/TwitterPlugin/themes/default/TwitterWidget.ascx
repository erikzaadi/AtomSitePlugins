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
        <% Html.RenderPartial("TwitterPartialWidget", Model.TwitterResponse.Tweets); %>
    </div>
    <% Html.BeginForm("Get", "Twitter", FormMethod.Get, new { id = "TwitterClientForm" }); %>
    <%= Html.Hidden("PagingIndex", Model.TwitterResponse.PagingIndex) %>
    <%= Html.Hidden("ClientRefreshDuration", Model.TwitterResponse.Settings.ClientRefreshDuration)%>
    <button type="submit" class="TwitterMoreButton TwitterRounded">
        <span class="TwitterMoreButtonText">More</span><span class="TwitterMoreButtonImage"><img
            src="<%= Url.ImageSrc("twitterloader.gif") %>" alt="Loading" /></span></button>
    <% Html.EndForm(); %>
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
