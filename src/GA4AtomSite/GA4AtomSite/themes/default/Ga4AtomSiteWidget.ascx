<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GA4AtomSite.Models.GA4AtomSiteModel>" %>
<%@ Import Namespace="GA.NET.MVC" %>
<%= !string.IsNullOrEmpty(Model.GAID) ? Html.GoogleAnalyticsHtmlHelper(Model.GAID) : "" %>