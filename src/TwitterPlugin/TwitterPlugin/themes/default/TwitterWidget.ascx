<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TwitterPluginForAtomSite.Models.ClientModel>" %>
<% 
    if (Model.TwitterResponse != null)
    {%>
<!-- Yes, inline style sucks -->
<style type="text/css">
    .TwitterMainContainer
    {
        background-color: #<%= Model.TwitterResponse.User.ProfileBackgroundColor %> !important;
    }
    .TwitterBottomNumber:hover, .TwitterBottomLabel, .TwitterMoreButtonText, .TwitterStatusTag a, .TwitterStatusLink a, .TwitterStatusTag a:visited, .TwitterStatusLink a:visited, .TwitterStatusMention a, .TwitterStatusMention a, .TwitterStatusMention a:visited, .TwitterStatusMention a:visited
    {
        color: #<%= Model.TwitterResponse.User.ProfileLinkColor %> !important;
    }
    .TwitterBottomInfo
    {
        background-color: #<%= Model.TwitterResponse.User.ProfileSideBarFillColor %> !important;
        border: 1px dotted #<%= Model.TwitterResponse.User.ProfileSideBarBorderColor %> !important;
    }
    .TwitterBottomNumber, .TwitterStatus
    {
        color: #<%= Model.TwitterResponse.User.ProfileTextColor %> !important;
    }
</style>
<div class="TwitterWidget">
    <h3>
        Twitter</h3>
    <div class="TwitterMainContainer TwitterRounded">
        <div class="TwitterTitle TwitterRounded">
            <div>
                <a href="<%= Model.TwitterResponse.User.ProfileURL %>" title="<%= Model.TwitterResponse.User.Description %>">
                    <img src="<%= Model.TwitterResponse.User.ImageURL %>" alt="Twitter Image" /></a><a
                        href="<%= Model.TwitterResponse.User.HomeURL %>" alt="<%= Model.TwitterResponse.User.HomeURL %>"
                        title="<%= Model.TwitterResponse.User.HomeURL %>" class="TwitterTitleName"><%= Model.TwitterResponse.User.Name%></a>
            </div>
            <span class="TwitterTitleNameSince">started tweeting
                <%= Html.DateTimeAgoAbbreviation(Model.TwitterResponse.User.HasBeenTweetingSince).Replace("on ","")%></span>
        </div>
        <div class="TwitterWhiteContainer TwitterRounded">
            <div class="TwitterStatuses TwitterRounded">
                <% Html.RenderPartial("TwitterPartialWidget", Model.TwitterResponse.Tweets); %>
            </div>
            <div>
                <% Html.BeginForm("Get", "Twitter", FormMethod.Get, new { id = "TwitterClientForm" }); %>
                <%= Html.Hidden("PagingIndex", Model.TwitterResponse.PagingIndex) %>
                <%= Html.Hidden("ClientRefreshDuration", Model.TwitterResponse.Settings.ClientRefreshDuration)%>
                <button type="submit" class="TwitterMoreButton TwitterRounded">
                    <span class="TwitterMoreButtonText">More</span><span class="TwitterMoreButtonImage"><img
                        src="<%= Url.ImageSrc("twitterloader.gif") %>" alt="Loading" /></span></button>
                <% Html.EndForm(); %>
            </div>
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
</div>
<%} %>
