<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<WLWWorkaround.WLWWorkaroundModel>" %>
<html>
<head>
    <title>Windows Live Workaround for AtomSite</title>
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            background-color: #F7F7F7;
        }
        .container
        {
            background-color: #FAFAFA;
            display: block !important;
            width: 500px;
            border: solid 1px #666;
            border-radius: 4px;
            -webkit-border-radius: 4px;
            -moz-border-radius: 4px;
            padding: 15px;
            margin-top: 100px;
            margin-left: auto;
            margin-right: auto;
        }
        #WLWWorkaround label
        {
            float: left;
            display: block;
            width: 135px;
        }
        #WLWWorkaround input, #WLWWorkaround button
        {
            float: none;
            clear: both;
        }
        #WLWWorkaround .errors, #WLWWorkaround .notifications
        {
            display: block !important;
            width: 400px;
            border: solid 1px #666;
            border-radius: 4px;
            -webkit-border-radius: 4px;
            -moz-border-radius: 4px;
            padding: 15px;
            background-color: Yellow;
        }
        
        #WLWWorkaround .errors
        {
            color: red;
        }
        
        #WLWWorkaround .notifications
        {
            color: Green;
        }
    </style>
</head>
<body>
    <div class="container">
        <h2>
            Windows Live Writer Workaround</h2>
        <h3>
            Enables to work with Windows Live Writer for a certain period
        </h3>
        <div id="WLWWorkaround">
            <% if (Model.HasErrorMessage)
               { %>
            <span class="errors">
                <%= Model.Message %>
            </span>
            <%}
               else if (Model.HasSuccessMessage)
               {%>
            <span class="notifications">
                <%= Model.Message %>
            </span>
            <%} %>
            <div>
                &nbsp;</div>
            <% if (Model.Active)
               { %>
            <h4>
                Disable the applied workaround</h4>
            <% using (Html.BeginForm("Disable", "WLWWorkaround", FormMethod.Post))
               { %>
            <div>
                <button type="submit">
                    Disable</button>
            </div>
            <%} %>
            <%}
               else
               { %>
            <h4>
                Credentials</h4>
            <% using (Html.BeginForm("Activate", "WLWWorkaround", FormMethod.Post))
               { %>
            <div>
                <label>
                    Username
                </label>
                <%= Html.TextBox("Username") %>
            </div>
            <div>
                <label>
                    Password
                </label>
                <%= Html.Password("Password") %>
            </div>
            <h4>
                Options</h4>
            <div>
                <label>
                    Expiration (Minutes)</label><%= Html.TextBox("ExpiresInMinutes", 20)%>
            </div>
            <div>
                <label>
                    &nbsp;</label>
                <button type="submit">
                    Submit</button>
            </div>
            <%} %>
            <%} %>
        </div>
    </div>
</body>
</html>
