<%@ Page Title="" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true"
    Inherits="ViewPage<GA4AtomSite.GA4AtomSiteAdminModel>" %>

<asp:Content ID="Body" ContentPlaceHolderID="content" runat="server">
    <h3>
        some title</h3>
    <p>
        <%= ViewData.Model.GAID %>
        registered successfully..
    </p>
</asp:Content>
