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

            var movieListRequestActive = false;
            var delay = 500;
            var delayTimeout;
        
            var filterKeyUp = function () {
                var $input = $(this);

                // Don't allow a request until the user has stopped typing for the "delay" amount
                if (delayTimeout) {
                    clearTimeout(delayTimeout);
                }

                delayTimeout = setTimeout(function () {
                    filterMovies($input.val());
                }, delay);
            };

            $('.movie-filter').find('input').keyup(filterKeyUp);

            var filterMovies = function (filterVal) {
                $.ajax('<%:Url.Action("MovieList") %>?filter=' + filterVal, {
                    type: 'get',
                    complete: function () {
                        movieListRequestActive = false;
                    },
                    success: function (data) {
                        $('#movieListContainer').empty().append(data);
                        rebindEvents();
                    }
                });
            };
        })
    
    </script>
    <div class="movie-filter">
        <strong>Filter Movies:</strong> <input type="text" />
    </div>
    <div id="movieListContainer">
        <%: Html.Partial("MovieList", Model) %>
    </div>
</asp:Content>
