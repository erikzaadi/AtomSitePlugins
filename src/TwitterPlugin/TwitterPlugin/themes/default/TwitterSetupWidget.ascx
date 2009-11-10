<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TwitterPluginForAtomSite.Models.SetupModel>" %>
<div class="TwitterSetupWidget widget settings area">
<h3>Twitter Widget Settings</h3>
    <%= Model.TwitterSettings != null ? Model.TwitterSettings.UserName : "Invalid Settings"%>
</div>
