<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TwitterPluginForAtomSite.Models.PublishModel>" %>
<div id="TwitterPublishWidget" class="TwitterSetupWidget widget settings area">
    <h3>
        Tweet your post!</h3>
    <% if (!Model.IsEnabled)
       { %>
    <div>
        Fill in your Twitter account details at the settings for the entire site to enable
        tweeting your posts</div>
    <%}
       else if (string.IsNullOrEmpty(Model.BlogIDString))
       {%>
       <div>You'll be able to tweet about this post once it's published..</div>
       <% } 
       else
       {%>
    <div>
        <div>
            <span class="TwitterSetupLabel">Template :</span><textarea cols="50" rows="3" name="statustemplate"><%= Model.DefaultStatusTemplate %></textarea>
            <div class="TwitterOptional">
                <%= Model.TitleTag %>
                will be replaced with the post title,<br />
                <%= Model.PostUrlTag %>
                will be replace with the post url</div>
        </div>
        <div>
            <button type="button">
                Tweet about post</button>&nbsp;<span class="TwitterSetupLabel" id="TwitterSetupMessage"></span>
        </div>
        <%= Html.Hidden("entryid", Model.BlogIDString)%>
        <%= Html.Hidden("url", Url.Action("Publish","Twitter")) %>
    </div>
    <%} %>
</div>
