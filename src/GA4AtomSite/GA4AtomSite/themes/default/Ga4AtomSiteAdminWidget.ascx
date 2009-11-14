<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GA4AtomSite.Models.GA4AtomSiteModel>" %>
<div class="GA4AtomSiteSetupWidget widget settings area">
    <h3>
        Google Analytics Widget Settings</h3>
    <% Html.BeginForm("Set", "GA4AtomSite", FormMethod.Post, new { id = "GA4AtomSiteSetupForm" });  %>
    <div>
        <div>
            <span class="GA4AtomSiteSetupLabel">Google Analytics Account ID:</span><%= Html.TextBox("GAID", Model.GAID)%>
        </div>
        <div>
            <span class="GA4AtomSiteSetupLabel" id="GA4AtomSiteSetupMessage"></span>&nbsp;<input type="submit"
                value="Update" />
        </div>
    </div>
    <% Html.EndForm();  %>
</div>
