<%@ Page Title="Title" Language="C#" Inherits="System.Web.Mvc.ViewPage<MovieTracker.Data.Movie>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <% using (Html.BeginForm())
       {
    %>
    <%:Html.AntiForgeryToken() %>
    <div class="deleteConfirm">
        <p>
            Are you sure you wante to delete the movie
            <strong><%:Model.Name %></strong>
            from your inventory?
        </p>
        <p>
            <%:Html.HiddenFor(m=>m.Id) %>
            <%:Html.ActionLink("No", "Detail", new{id = Model.Id}) %> | 
            <input type="submit" value="Yes" />
        </p>
    </div>
    <%     
   } %>
</asp:Content>
