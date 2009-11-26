<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TwitterPluginForAtomSite.Models.PublishModel>" %>
<div class="TwitterSetupWidget widget settings area">
    <h3>
        Twitter Widget Settings</h3>
    <% Html.BeginForm("Publish", "Twitter", FormMethod.Post, new { id = "TwitterPublishForm" });  %>
    <div>
        <div>
            <span class="TwitterSetupLabel" id="TwitterSetupMessage"></span>&nbsp;<input type="submit"
                value="Tweet about post" />
        </div>
    </div>
    <% Html.EndForm();  %>
</div>
