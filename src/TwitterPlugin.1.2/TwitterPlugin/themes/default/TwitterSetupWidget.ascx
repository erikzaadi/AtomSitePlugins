<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TwitterPluginForAtomSite.Models.SetupModel>" %>
<div class="TwitterSetupWidget widget settings area">
    <h3>
        Twitter Widget Settings</h3>
    <% Html.BeginForm("Setup", "Twitter", FormMethod.Post, new { id = "TwitterSetupForm" });  %>
    <div>
        <div>
            <span class="TwitterSetupLabel">User Name:</span><%= Html.TextBox("id",  Model.TwitterSettings != null ? Model.TwitterSettings.UserName : "") %>
        </div>
        <div>
            <span class="TwitterSetupLabel">Tweets to get:</span><%= Html.TextBox("limit",  Model.TwitterSettings != null ? Model.TwitterSettings.Limit : null)%>
            <div class="TwitterOptional">Optional, Defaults to
                <%= TwitterPluginForAtomSite.TwitterStructs.TwitterConsts.TwitterDefaultLimit %>
                Tweets</div>
        </div>
        <div>
            <span class="TwitterSetupLabel">Cache Duration (Minutes):</span><%= Html.TextBox("cachedurationinminutes", Model.TwitterSettings != null && Model.TwitterSettings.CacheDuration.HasValue ? Model.TwitterSettings.CacheDuration.Value.Minutes.ToString() : "")%>
            <div class="TwitterOptional">Optional , Defaults to
                <%= TwitterPluginForAtomSite.TwitterStructs.TwitterConsts.TwitterDefaultCacheDuration.Minutes %>
                minutes </div>
        </div>
        <div>
            <span class="TwitterSetupLabel" id="TwitterSetupMessage"></span>&nbsp;<input type="submit"
                value="Update" />
        </div>
    </div>
    <% Html.EndForm();  %>
</div>
