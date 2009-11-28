<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TwitterPluginForAtomSite.Models.SetupModel>" %>
<div class="TwitterSetupWidget widget settings area">
    <h3>
        Twitter Widget Settings</h3>
    <% Html.BeginForm("Setup", "Twitter", FormMethod.Post, new { id = "TwitterSetupForm" });  %>
    <div>
        <div>
            <span class="TwitterSetupLabel">User Name:</span><%= Html.TextBox("id",  Model.TwitterSettings != null ? Model.TwitterSettings.UserName : "") %>
        </div>
        <fieldset class="TwitterRounded">
            <legend>Optional: </legend>
            <div class="TwitterSetupOptionalContent">
                <div>
                    <span class="TwitterSetupLabel">Tweets to get:</span><%= Html.TextBox("limit",  Model.TwitterSettings != null ? Model.TwitterSettings.Limit : null)%>
                    <div class="TwitterOptional">
                        Defaults to
                        <%= TwitterPluginForAtomSite.TwitterStructs.TwitterConsts.TwitterDefaultLimit %>
                        Tweets</div>
                </div>
                <div>
                    <span class="TwitterSetupLabel">Cache Duration (Minutes):</span><%= Html.TextBox("cachedurationinminutes", Model.TwitterSettings != null && Model.TwitterSettings.CacheDuration.HasValue ? Model.TwitterSettings.CacheDuration.Value.ToString() : "")%>
                    <div class="TwitterOptional">
                        Defaults to
                        <%= TwitterPluginForAtomSite.TwitterStructs.TwitterConsts.TwitterDefaultCacheDuration %>
                        minutes
                    </div>
                </div>
                <div>
                    <span class="TwitterSetupLabel">Client Refresh (Minutes):</span><%= Html.TextBox("clientrefreshinminutes", Model.TwitterSettings != null && Model.TwitterSettings.ClientRefreshDuration.HasValue ? Model.TwitterSettings.ClientRefreshDuration.Value.ToString() : "")%>
                    <div class="TwitterOptional">
                        Defaults to
                        <%= TwitterPluginForAtomSite.TwitterStructs.TwitterConsts.TwitterDefaultClientRefreshDuration %>
                        minutes
                    </div>
                </div>
                <div>
                    <span class="TwitterSetupLabel">Password :</span><%= Html.Password("password", "*******")%>
                    <div class="TwitterOptional">
                        Used for publishing post in a tweet
                    </div>
                </div>
            </div>
        </fieldset>
        <div>
            <span class="TwitterSetupLabel" id="TwitterSetupMessage"></span>&nbsp;<input type="submit"
                value="Update" />
        </div>
    </div>
    <% Html.EndForm();  %>
</div>
