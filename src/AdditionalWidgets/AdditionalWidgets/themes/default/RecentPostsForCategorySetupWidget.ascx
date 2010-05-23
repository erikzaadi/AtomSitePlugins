<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AdditionalWidgets.AdditionalWidgetsModels.RecentPostsForCategoryWidgetSetupConfigModel>" %>
<% if (Model.Collections == null
       || Model.Collections.Count() == 0
       || Model.CollectionCategories == null
       || Model.CollectionCategories.Count() == 0)
   { %>
<div id="errors">
    <h3 class="error">
        No categories found..</h3>
</div>
<div class="buttons">
    <button type="button" name="close">
        Cancel</button>
</div>
<%}
   else
   { %>
<% using (Html.BeginForm("RecentPostsForCategorySetupWidget", "AdditionalWidgets", FormMethod.Post, new { id = "configureForm", @class = "choose" }))
   { %>
<fieldset>
    <h3>
        Choose a Category</h3>
    <%= Html.ValidationSummary() %>
    <%= Html.Hidden("IncludePath", Model.IncludePath) %>
    <label>
        Collection
        <%= Html.DropDownList("Collection", Model.Collections) %>
    </label>
    <%= Html.Hidden("Category") %>
    <label>
        Category
        <% foreach (var collectionCategories in Model.CollectionCategories)
           { %>
        <%= Html.DropDownList(collectionCategories.Collection , collectionCategories.Categories, new { @class="selectCategories", style="display:none" }) %>
        <%} %>
    </label>
    <div>
        <label for="Count">
            Count <small>maximum entries to show, or zero for default</small></label>
        <%= Html.TextBox("Count", Model.Count, new { style="width:5em", maxlength = 8} ) %></div>
    <div class="buttons">
        <button type="button" name="close">
            Cancel</button>
        <input type="submit" value="Save" />
    </div>
</fieldset>
<%}%>
<script type="text/javascript">
    $(document).ready(function () {
        $("#Collection")
	.change(function () {
	    var current = this.value;
	    $('.selectCategories').each(function () {
	        var $sel = $(this);
	        if (current == $sel.attr('name'))
	            $sel.show();
	        else
	            $sel.hide();
	    });
	})
	.change();
        $("#configureForm").submit(function () {
            $("#Category").val($(".selectCategories:visible").val());
            return true;
        });
    });
</script>
<%} %>