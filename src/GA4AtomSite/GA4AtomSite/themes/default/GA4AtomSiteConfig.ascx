<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GA4AtomSite.GA4AtomSiteConfigModel>" %>
<% using (Html.BeginForm("Config", "GA4AtomSite", FormMethod.Post, new { id = "configureForm" }))
   { %>
<fieldset>
    <%= Html.Hidden("includePath", Model.IncludePath) %>
    <h3>
        Google Analytics Widget Configuration</h3>
    <%= Html.ValidationSummary() %>
    <div>
        <label for="GoogleAccountID">
            Google Analytics Account ID <small><span style="color: red">*</span> UA-XXXXXXX-X</small></label>
        <%= Html.TextBox("GoogleAccountID", Model.GoogleAccountID, new { style = "width:10em", maxlength = 100 })%></div>
    <div class="buttons">
        <button type="button" name="close">
            Cancel</button>
        <input type="submit" value="Save" />
    </div>
</fieldset>
<%}%>