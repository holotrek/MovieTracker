<%@ Page Title="Title" Language="C#" Inherits="System.Web.Mvc.ViewPage<MovieTracker.Models.EditMovieViewModel>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>"
        type="text/javascript"></script>
    <% using (Html.BeginForm())
       { %>
    <div class="movieInput">
        <h1>Edit Movie - <%:Model.Name %></h1>
        <%:Html.ValidationSummary()%>
        <%:Html.AntiForgeryToken()%>
        <%:Html.HiddenFor(m => m.Id)%>
        <table>
            <tbody>
                <tr>
                    <td>
                        <%:Html.LabelFor(m => m.Name)%>
                    </td>
                    <td>
                        <%:Html.TextBoxFor(m => m.Name)%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%:Html.LabelFor(m => m.Stars)%>
                    </td>
                    <td>
                        <%:Html.TextBoxFor(m => m.Stars)%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%:Html.LabelFor(m => m.GenreId)%>
                    </td>
                    <td>
                        <%:Html.DropDownListFor(m => m.GenreId, Model.Genres)%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%:Html.LabelFor(m => m.Rating)%>
                    </td>
                    <td>
                        <%:Html.DropDownListFor(m => m.Rating, Model.RatingOptions)%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%:Html.LabelFor(m => m.Directors)%>
                    </td>
                    <td>
                        <%:Html.TextBoxFor(m => m.Directors)%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%:Html.LabelFor(m => m.Writers)%>
                    </td>
                    <td>
                        <%:Html.TextBoxFor(m => m.Writers)%>
                    </td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="4" class="navInput">
                        <%:Html.ActionLink("Back", "Detail", new {id = Model.Id})%>
                        <input type="submit" value="Save" />
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
    <% } %>
</asp:Content>
