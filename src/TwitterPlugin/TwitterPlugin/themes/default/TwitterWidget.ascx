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
							<% if (p.InReplyTo != null) 
								{ %>
									<div class="TwitterInResponseTo">
										In reply to <a href="<%= p.InReplyToStatusURL %>"><%= p.InReplyTo.Name %></a>..
									</div>
								<% } %>
							<div class="TwitterStatusTime">
								<%= Html.DateTimeAgoAbbreviation(p.CreatedAt) %>
							</div>
						</div>
				<% } %>
			</div>	
<%} %>
