using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using MovieTracker.Data;
using MovieTracker.Models;
using MovieTracker.Services;
using System.Net;

namespace MovieTracker.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        private readonly IRepository<Genre> _genreRepository;
        private readonly IMembershipService _membershipService;
        private readonly IRepository<Movie> _movieRepository;

        public MovieController(IRepository<Movie> movieRepository, IRepository<Genre> genreRepository, IMembershipService membershipService)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
            _membershipService = membershipService;
        }

        public ActionResult Index()
        {
            MembershipUser user = _membershipService.GetUser(User.Identity.Name);

            if (user == null)
            {
                return RedirectToAction("Register", "Account");
            }

            var userKey = (Guid) user.ProviderUserKey;

            IEnumerable<Movie> movies = _movieRepository.Find(m => m.aspnet_UsersUserId == userKey);

            return View(movies);
        }

        public ActionResult Detail(int id)
        {
            Movie movie = _movieRepository.Find(m => m.Id == id).FirstOrDefault();
            if (movie == null)
            {
                return RedirectToAction("MoveNotFound");
            }

            MembershipUser user = _membershipService.GetUser(User.Identity.Name);
            if (!movie.IsOwnedBy(user))
            {
                return RedirectToAction("MovieNotAuthorized");
            }

            return View(movie);
        }

        public ActionResult Edit(int id)
        {
            Movie movie = _movieRepository.Find(m => m.Id == id).FirstOrDefault();

            if (movie == null)
            {
                return RedirectToAction("MoveNotFound");
            }

            MembershipUser user = _membershipService.GetUser(User.Identity.Name);
            if (!movie.IsOwnedBy(user))
            {
                return RedirectToAction("MovieNotAuthorized");
            }

            IEnumerable<Genre> genres = _genreRepository.GetAll();

            var editMovieViewModel = new EditMovieViewModel
                                         {
                                             Directors = movie.Directors,
                                             Genres = new SelectList(genres, "Id", "Name"),
                                             Name = movie.Name,
                                             Rating = movie.Rating,
                                             Stars = movie.Stars,
                                             Writers = movie.Writers,
                                             Id = id,
                                             GenreId = movie.GenreId
                                         };


            return View(editMovieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditMovieViewModel editMovieViewModel)
        {
            if (ModelState.IsValid)
            {
                Movie movie = _movieRepository.Find(m => m.Id == editMovieViewModel.Id).FirstOrDefault();

                if (movie == null)
                {
                    return RedirectToAction("MoveNotFound");
                }

                MembershipUser user = _membershipService.GetUser(User.Identity.Name);
                if (!movie.IsOwnedBy(user))
                {
                    return RedirectToAction("MovieNotAuthorized");
                }

                movie.Directors = editMovieViewModel.Directors;
                movie.GenreId = editMovieViewModel.GenreId;
                movie.Name = editMovieViewModel.Name;
                movie.Rating = editMovieViewModel.Rating;
                movie.Stars = editMovieViewModel.Stars;
                movie.Writers = editMovieViewModel.Writers;

                _movieRepository.Save();
                return RedirectToAction("Index");
            }
            IEnumerable<Genre> genres = _genreRepository.GetAll();
            editMovieViewModel.Genres = new SelectList(genres, "Id", "Name");

            return View(editMovieViewModel);
        }

        public ActionResult Add()
        {
            IEnumerable<Genre> genres = _genreRepository.GetAll();

            var editMovieViewModel = new EditMovieViewModel
                                         {
                                             Genres = new SelectList(genres, "Id", "Name"),
                                         };

            return View(editMovieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(EditMovieViewModel editMovieViewModel)
        {
            if (ModelState.IsValid)
            {
                MembershipUser user = _membershipService.GetUser(User.Identity.Name);
                if (user == null)
                {
                    return RedirectToAction("Register", "Account");
                }
                var userKey = (Guid) user.ProviderUserKey;

                var movie = new Movie
                                {
                                    Directors = editMovieViewModel.Directors,
                                    GenreId = editMovieViewModel.GenreId,
                                    Name = editMovieViewModel.Name,
                                    Rating = editMovieViewModel.Rating,
                                    Stars = editMovieViewModel.Stars,
                                    Writers = editMovieViewModel.Writers,
                                    aspnet_UsersUserId = userKey
                                };

                _movieRepository.Add(movie);
                _movieRepository.Save();
                return RedirectToAction("Index");
            }
            IEnumerable<Genre> genres = _genreRepository.GetAll();
            editMovieViewModel.Genres = new SelectList(genres, "Id", "Name");

            return View(editMovieViewModel);
        }

        public ActionResult Delete(int id)
        {
            Movie movie = _movieRepository.Find(m => m.Id == id).FirstOrDefault();

            if (movie == null)
            {
                return RedirectToAction("MoveNotFound");
            }

            MembershipUser user = _membershipService.GetUser(User.Identity.Name);
            if (!movie.IsOwnedBy(user))
            {
                return RedirectToAction("MovieNotAuthorized");
            }

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Movie movie)
        {
            Movie movieToDelete = _movieRepository.Find(m => m.Id == movie.Id).FirstOrDefault();

            MembershipUser user = _membershipService.GetUser(User.Identity.Name);
            if (!movieToDelete.IsOwnedBy(user))
            {
                return RedirectToAction("MovieNotAuthorized");
            }

            _movieRepository.Delete(movieToDelete);
            _movieRepository.Save();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ClearRating(int movieId)
        {
            Movie movie = _movieRepository.Find(m => m.Id == movieId).Single();
            movie.ClearRating();

            MembershipUser user = _membershipService.GetUser(User.Identity.Name);
            if (!movie.IsOwnedBy(user))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            _movieRepository.Save();

            return View("MovieRatingControl", movie);
        }

        [HttpPost]
        public ActionResult AddRating(int movieId, short rating)
        {
            Movie movie = _movieRepository.Find(m => m.Id == movieId).Single();

            MembershipUser user = _membershipService.GetUser(User.Identity.Name);
            if (!movie.IsOwnedBy(user))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            movie.Rating = rating;

            _movieRepository.Save();

            return View("MovieRatingControl", movie);
        }

        public ActionResult MoveNotFound()
        {
            return View();
        }

        public ActionResult MovieNotAuthorized()
        {
            return View();
        }
    }
}