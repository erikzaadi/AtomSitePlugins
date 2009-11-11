<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TwitterPluginForAtomSite.Models.SetupModel>" %>
<div class="TwitterSetupWidget widget settings area">
    <h3>
        Twitter Widget Settings</h3>
    <% Html.BeginForm("Setup", "Twitter", FormMethod.Post, new { id = "TwitterSetupForm" });  %>
    <div>
        <div>
            <span class="TwitterSetupLabel">User Name:</span><%= Html.TextBox("id",  Model.TwitterSettings != null ? Model.TwitterSettings.UserName : "") %>&nbsp;<span id="TwitterSetupMessage"></span>
        </div>
        <div>
            <span class="TwitterSetupLabel">Tweet Limit:</span><%= Html.TextBox("limit",  Model.TwitterSettings != null ? Model.TwitterSettings.Limit : null)%>
            <input type="submit" value="Update" />
        </div>
    </div>
    <% Html.EndForm();  %>
</div>
