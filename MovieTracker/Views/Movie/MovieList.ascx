<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<MovieTracker.Data.Movie>>" %>

<table class="movieList">
    <thead>
        <tr>
            <th>
                Edit
            </th>
            <th>
                Name
            </th>
            <th>
                Rating
            </th>
            <th>
                Delete
            </th>
        </tr>
    </thead>
    <tbody>
<% foreach (var movie in Model)
    {
%>
<tr>
    <td>
        <a href="<%:Url.Action("Edit", new { id = movie.Id }) %>" class="edit" title="Edit <%:movie.Name %>"></a>
    </td>
    <td>
        <%:Html.ActionLink(movie.Name, "Detail", new {id=movie.Id}) %>
    </td>
    <td>
        <%: Html.Partial("MovieRatingControl", movie) %>
    </td>
    <td>
        <a href="<%:Url.Action("Delete", new { id = movie.Id }) %>" class="delete" title="Delete <%:movie.Name %>"></a>
    </td>
</tr>
<%
    } 
%>
    </tbody>
    <tfoot>
        <tr>
            <td colspan="4">
                <%:Html.ActionLink("Add new movie", "Add") %>
            </td>
        </tr>
    </tfoot>
</table>