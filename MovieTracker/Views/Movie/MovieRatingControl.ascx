<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MovieTracker.Data.Movie>" %>
<div data-movieid="<%:Model.Id %>" class="movieRating">
    <div class="star <%:Model.Rating >= 1 ? "rated" : ""%>">
    </div>
    <div class="star <%:Model.Rating >= 2 ? "rated" : ""%>">
    </div>
    <div class="star <%:Model.Rating >= 3 ? "rated" : ""%>">
    </div>
    <div class="star <%:Model.Rating >= 4 ? "rated" : ""%>">
    </div>
    <div class="star <%:Model.Rating >= 5 ? "rated": ""%>">
    </div>
    <div class="clearRating">Clear Rating</div>
</div>
