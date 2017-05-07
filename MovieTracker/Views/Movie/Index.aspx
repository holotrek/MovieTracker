<%@ Page Title="Title" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<MovieTracker.Data.Movie>>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
    My Movies</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">

    <script type="text/javascript">
        $(function () {
            var clearRating = function () {
                var $td = $(this).closest("td");
                var movieToClearRating = {
                    movieId: $(this).closest(".movieRating").data('movieid')
                };
                $.ajax('<%:Url.Action("ClearRating") %>', {
                    type: 'post',
                    data: movieToClearRating,
                    success: function (data) {
                        $td.empty().append(data);
                        rebindEvents($td);
                    },
                    error: function (err) {
                        alert('Error clearing movie rating: ' + err.statusText);
                    }
                });
            };

            var changeRating = function () {
                var $td = $(this).closest("td");
                var rating = $(this).index() + 1;

                var movieToRate = {
                    movieId: $(this).closest(".movieRating").data('movieid'),
                    rating : rating
                };

                $.ajax('<%:Url.Action("AddRating") %>', {
                    type: 'post',
                    data: movieToRate,
                    success: function (data) {
                        $td.empty().append(data);
                        rebindEvents($td);
                    },
                    error: function (err) {
                        alert('Error changing movie rating: ' + err.statusText);
                    }
                });
            };

            var hoverRating = function () {
                $(this).prevAll(".star").addBack().addClass("hovered");
            };
            var unHoverRating = function () {
                $(this).siblings(".star").addBack().removeClass("hovered");
            };

            var rebindEvents = function ($td) {
                $(".clearRating", $td).click(clearRating);
                $(".star", $td).click(changeRating);
                $(".star", $td).hover(hoverRating, unHoverRating);
            };

            $(".clearRating").click(clearRating);
            $(".star").click(changeRating);
            $(".star").hover(hoverRating, unHoverRating);
        })
    
    </script>

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
               } %>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="4">
                    <%:Html.ActionLink("Add new movie", "Add") %>
                </td>
            </tr>
        </tfoot>
    </table>
</asp:Content>
