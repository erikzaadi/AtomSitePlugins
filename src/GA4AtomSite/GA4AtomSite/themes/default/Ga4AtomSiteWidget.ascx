<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GA4AtomSite.GA4AtomSiteModel>" %>
<%@ Import Namespace="GA.NET.MVC" %>
<%= !string.IsNullOrEmpty(Model.GoogleAccountID) ? Html.GoogleAnalytics(Model.GoogleAccountID) : ""%>