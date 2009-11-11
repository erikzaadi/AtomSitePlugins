<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TwitterPluginForAtomSite.Models.ClientModel>" %>
<% 
	if (Model.TwitterResponse != null)
		{%>
			<div class="TwitterWidget">
				<div class="TwitterTitle">
					<a href="<%= Model.TwitterResponse.User.TwitterHomeURL %>"><%= Model.TwitterResponse.User.Name%></a>
				</div>
				<% foreach (var p in Model.TwitterResponse.Tweets)
					{ %>
						<div class="TwitterStatus">
							<div class="TwitterStatusText"><%= p.Text %></div>
							<div class="TwitterStatusFooter">
							<span class="TwitterStatusTime">
								<%= Html.DateTimeAgoAbbreviation(p.CreatedAt) %>
							</span>&nbsp;from
							<span class="TwitterStatusSource">
								<%= p.Source %>
							</span>&nbsp;
							<% if (!string.IsNullOrEmpty(p.InReplyToScreenName)) 
								{ %>
									<span class="TwitterInResponseTo">
										<a href="<%= p.InReplyToStatusURL %>">In reply to <%= p.InReplyToScreenName%></a>..
									</span>
								<% } %>
							</div>
						</div>
				<% } %>
			</div>	
<%} %>
