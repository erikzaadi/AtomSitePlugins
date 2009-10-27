<%@ Page Title="" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true"
    Inherits="ViewPage<GA4AtomSite.GA4AtomSiteAdminModel>" %>

<asp:Content ID="Body" ContentPlaceHolderID="content" runat="server">
    <h3>
        some title</h3>
    <p>
        input from here, then submit to a route?
        <%= string.IsNullOrEmpty(ViewData.Model.GAID) ? ViewData.Model.GAID : "Key needed"%>
    </p>
    <div>
        <% Html.BeginForm("Set", "GA4AtomSite", null, FormMethod.Post, new { id = "GA4AtomSiteForm" }); %>
        <%= Html.TextBox("GAID", ViewData.Model.GAID)%>
        <% Html.EndForm(); %>
    </div>
</asp:Content>
