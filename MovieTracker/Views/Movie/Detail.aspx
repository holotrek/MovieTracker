<%@ Page Title="Title" Language="C#" Inherits="System.Web.Mvc.ViewPage<MovieTracker.Data.Movie>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <div class="movieDetail">
            <a href="<%:Url.Action("Delete", new { id = Model.Id }) %>" class="delete" title="Delete <%:Model.Name %>"></a>
            <a href="<%:Url.Action("Edit", new { id = Model.Id }) %>" class="edit" title="Edit <%:Model.Name %>"></a>
        <h1>
            <%:Model.Name%></h1>
        <%:Model.Stars%>
        <fieldset>
            <legend>Movie Details</legend>
            <table>
                <tbody>
                    <tr>
                        <td>
                            <%:Html.LabelFor(m => m.GenreId)%>
                        </td>
                        <td>
                            <%:Model.Genre.Name%>
                        </td>
                        <td>
                            <%:Html.LabelFor(m => m.Rating)%>
                        </td>
                        <td>
                            <%:Model.Rating%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%:Html.LabelFor(m => m.Directors)%>
                        </td>
                        <td>
                            <%:Model.Directors%>
                        </td>
                        <td>
                            <%:Html.LabelFor(m => m.Writers)%>
                        </td>
                        <td>
                            <%:Model.Writers%>
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                </tfoot>
            </table>
        </fieldset>
        <div class="navInput">
            <%:Html.ActionLink("Back", "Index") %>
        </div>
    </div>
</asp:Content>
