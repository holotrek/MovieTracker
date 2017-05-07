using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieTracker.Controllers;
using MovieTracker.Data;
using MovieTracker.Models;
using MovieTracker.Services;
using MovieTracker.Tests.Unit.Fakes;

namespace MovieTracker.Tests.Unit.Controllers
{
    [TestClass]
    public class MovieControllerTests
    {
        [TestMethod]
        public void Index_WithNullUser_RedirectsToAccountRegister()
        {
            IMembershipService membershipService = new FakeMembershipService(Enumerable.Empty<MembershipUser>());

            var movieController = new MovieController(null, null, membershipService)
                                      {
                                          ControllerContext = new FakeControllerContext()
                                      };


            var result = (RedirectToRouteResult) movieController.Index();
            
            Assert.IsTrue(string.Compare(result.Action(), "register", true) == 0);
            Assert.IsTrue(string.Compare(result.Controller(), "account", true) == 0);
        }

        [TestMethod]
        public void Index_WithValidUser_ReturnsView()
        {
            var users = new List<MembershipUser>();

            Guid guid = Guid.NewGuid();
            users.Add(new TestMembershipUser("test", guid));

            IMembershipService membershipService = new FakeMembershipService(users);

            IRepository<Movie> movieRepository = new FakeMovieRepository();
            var movieController = new MovieController(movieRepository, null, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            var result = movieController.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Detail_WithNoMovieFound_RedirectsToMovieNotFound()
        {
            IMembershipService membershipService = new FakeMembershipService(Enumerable.Empty<MembershipUser>());

            IRepository<Movie> movieRepository = new FakeMovieRepository();
            var movieController = new MovieController(movieRepository, null, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            var result = (RedirectToRouteResult) movieController.Detail(42);

            Assert.IsTrue(string.Compare(result.Action(), "MoveNotFound", true) == 0);
        }

        [TestMethod]
        public void Detail_WithMovieFoundNotOwnedByUser_RedirectsToMovieNotAuthorized()
        {
            IMembershipService membershipService = new FakeMembershipService(Enumerable.Empty<MembershipUser>());

            var movies = new List<Movie>();
            const int movieId = 42;
            movies.Add(new Movie { Id = movieId });
            IRepository<Movie> movieRepository = new FakeMovieRepository(movies);
            var movieController = new MovieController(movieRepository, null, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            var result = (RedirectToRouteResult)movieController.Detail(42);

            Assert.IsTrue(string.Compare(result.Action(), "MovieNotAuthorized", true) == 0);
        }

        [TestMethod]
        public void Detail_WithMovieFoundOwnedByUser_ReturnsView()
        {
            Guid guid = Guid.NewGuid();
            var users = new List<MembershipUser>();
            users.Add(new TestMembershipUser("test", guid));
            IMembershipService membershipService = new FakeMembershipService(users);

            var movies = new List<Movie>();
            const int movieId = 42;
            movies.Add(new Movie
            {
                Id = movieId,
                aspnet_UsersUserId = guid
            });

            IRepository<Movie> movieRepository = new FakeMovieRepository(movies);
            var movieController = new MovieController(movieRepository, null, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            var result = movieController.Detail(movieId) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Edit_WithNoMovieFound_RedirectsToMovieNotFound()
        {
            IMembershipService membershipService = new FakeMembershipService(Enumerable.Empty<MembershipUser>());

            IRepository<Movie> movieRepository = new FakeMovieRepository();
            var movieController = new MovieController(movieRepository, null, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            var result = (RedirectToRouteResult) movieController.Edit(42);

            Assert.IsTrue(string.Compare(result.Action(), "MoveNotFound", true) == 0);
        }

        [TestMethod]
        public void Edit_WithMovieFoundNotOwnedByUser_RedirectsToMovieNotAuthorized()
        {
            IMembershipService membershipService = new FakeMembershipService(Enumerable.Empty<MembershipUser>());

            var movies = new List<Movie>();
            const int movieId = 42;
            movies.Add(new Movie { Id = movieId });
            IRepository<Movie> movieRepository = new FakeMovieRepository(movies);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            var result = (RedirectToRouteResult)movieController.Edit(movieId);

            Assert.IsTrue(string.Compare(result.Action(), "MovieNotAuthorized", true) == 0);
        }

        [TestMethod]
        public void Edit_WithMovieFoundOwnedByUser_ReturnsView()
        {
            Guid guid = Guid.NewGuid();
            var users = new List<MembershipUser>();
            users.Add(new TestMembershipUser("test", guid));
            IMembershipService membershipService = new FakeMembershipService(users);

            var movies = new List<Movie>();
            const int movieId = 42;
            movies.Add(new Movie
            {
                Id = movieId,
                aspnet_UsersUserId = guid
            });

            IRepository<Movie> movieRepository = new FakeMovieRepository(movies);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            var result = movieController.Edit(movieId) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Edit_WithMovieToEditNotFound_RedirectsToMovieNotFound()
        {
            IMembershipService membershipService = new FakeMembershipService(Enumerable.Empty<MembershipUser>());

            var movies = new List<Movie>();
            IRepository<Movie> movieRepository = new FakeMovieRepository(movies);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            var result = (RedirectToRouteResult) movieController.Edit(new EditMovieViewModel());

            Assert.IsTrue(string.Compare(result.Action(), "MoveNotFound", true) == 0);
        }

        [TestMethod]
        public void Edit_WithMovieToEditFoundNotOwnedByUser_RedirectsToMovieNotAuthorized()
        {
            IMembershipService membershipService = new FakeMembershipService(Enumerable.Empty<MembershipUser>());

            var movies = new List<Movie>();
            const int movieId = 42;
            movies.Add(new Movie { Id = movieId });
            IRepository<Movie> movieRepository = new FakeMovieRepository(movies);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            var editMovieViewModel = new EditMovieViewModel
            {
                Id = movieId,
                Directors = "Directors",
                GenreId = 1,
                Name = "Name",
                Rating = 3,
                Stars = "Stars",
                Writers = "Writers"
            };

            var result = (RedirectToRouteResult)movieController.Edit(editMovieViewModel);

            Assert.IsTrue(string.Compare(result.Action(), "MovieNotAuthorized", true) == 0);
        }

        [TestMethod]
        public void Edit_WithMovieToEditFoundOwnedByUser_RedirectsToIndex()
        {
            Guid guid = Guid.NewGuid();
            var users = new List<MembershipUser>();
            users.Add(new TestMembershipUser("test", guid));
            IMembershipService membershipService = new FakeMembershipService(users);

            var movies = new List<Movie>();
            const int movieId = 42;
            movies.Add(new Movie
            {
                Id = movieId,
                aspnet_UsersUserId = guid
            });

            IRepository<Movie> movieRepository = new FakeMovieRepository(movies);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            var editMovieViewModel = new EditMovieViewModel
            {
                Id = movieId,
                Directors = "Directors",
                GenreId = 1,
                Name = "Name",
                Rating = 3,
                Stars = "Stars",
                Writers = "Writers"
            };

            var result = (RedirectToRouteResult)movieController.Edit(editMovieViewModel);

            Assert.IsTrue(string.Compare(result.Action(), "Index", true) == 0);
        }

        [TestMethod]
        public void Edit_WithInvalidModelState_ReturnsView()
        {
            IMembershipService membershipService = new FakeMembershipService(Enumerable.Empty<MembershipUser>());

            var movies = new List<Movie>();
            const int movieId = 42;
            movies.Add(new Movie {Id = movieId});
            IRepository<Movie> movieRepository = new FakeMovieRepository(movies);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            //setup model error to trigger !IsValid
            movieController.ModelState.AddModelError("key", "errormessage");

            var result = movieController.Edit(new EditMovieViewModel()) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Add_ReturnsView()
        {
            IMembershipService membershipService = new FakeMembershipService(Enumerable.Empty<MembershipUser>());

            IRepository<Movie> movieRepository = new FakeMovieRepository();
            IRepository<Genre> genreRepository = new FakeGenreRepository();

            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            var result = movieController.Add() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Add_WithInvalidModelState_ReturnsView()
        {
            IMembershipService membershipService = new FakeMembershipService(Enumerable.Empty<MembershipUser>());

            var movies = new List<Movie>();
            const int movieId = 42;
            movies.Add(new Movie {Id = movieId});
            IRepository<Movie> movieRepository = new FakeMovieRepository(movies);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            //setup model error to trigger !IsValid
            movieController.ModelState.AddModelError("key", "errormessage");

            var result = movieController.Add(new EditMovieViewModel()) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Add_WithUserNotFound_ReturnsRedirectsToAccountRegister()
        {
            IMembershipService membershipService = new FakeMembershipService(Enumerable.Empty<MembershipUser>());

            var movies = new List<Movie>();
            const int movieId = 42;
            movies.Add(new Movie {Id = movieId});
            IRepository<Movie> movieRepository = new FakeMovieRepository(movies);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            var result = (RedirectToRouteResult) movieController.Add(new EditMovieViewModel());

            Assert.IsTrue(string.Compare(result.Action(), "register", true) == 0);
            Assert.IsTrue(string.Compare(result.Controller(), "account", true) == 0);
        }

        [TestMethod]
        public void Add_WithValidUserAndModel_CallsAddOnMovieRepository()
        {
            var users = new List<MembershipUser>();
            Guid guid = Guid.NewGuid();
            users.Add(new TestMembershipUser("test", guid));

            IMembershipService membershipService = new FakeMembershipService(users);

            var movies = new List<Movie>();
            const int movieId = 42;
            movies.Add(new Movie {Id = movieId});

            bool wasCalled = false;
            Action addAction = () => wasCalled = true;

            IRepository<Movie> movieRepository = new FakeMovieRepository(movies, addCallback: addAction);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            var editMovieViewModel = new EditMovieViewModel
                                         {
                                             Id = movieId,
                                             Directors = "Directors",
                                             GenreId = 1,
                                             Name = "Name",
                                             Rating = 3,
                                             Stars = "Stars",
                                             Writers = "Writers"
                                         };

            movieController.Add(editMovieViewModel);

            Assert.IsTrue(wasCalled);
        }

        [TestMethod]
        public void Add_WithValidUserAndModel_CallsSaveOnMovieRepository()
        {
            var users = new List<MembershipUser>();
            Guid guid = Guid.NewGuid();
            users.Add(new TestMembershipUser("test", guid));

            IMembershipService membershipService = new FakeMembershipService(users);

            var movies = new List<Movie>();
            const int movieId = 42;
            movies.Add(new Movie {Id = movieId});

            bool wasCalled = false;
            Action saveAction = () => wasCalled = true;

            IRepository<Movie> movieRepository = new FakeMovieRepository(movies, saveCallback: saveAction);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            var editMovieViewModel = new EditMovieViewModel
                                         {
                                             Id = movieId,
                                             Directors = "Directors",
                                             GenreId = 1,
                                             Name = "Name",
                                             Rating = 3,
                                             Stars = "Stars",
                                             Writers = "Writers"
                                         };

            movieController.Add(editMovieViewModel);

            Assert.IsTrue(wasCalled);
        }

        [TestMethod]
        public void Add_WithValidUserAndModel_RedirectsToIndex()
        {
            var users = new List<MembershipUser>();
            Guid guid = Guid.NewGuid();
            users.Add(new TestMembershipUser("test", guid));

            IMembershipService membershipService = new FakeMembershipService(users);

            var movies = new List<Movie>();
            const int movieId = 42;
            movies.Add(new Movie {Id = movieId});

            IRepository<Movie> movieRepository = new FakeMovieRepository(movies);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            var editMovieViewModel = new EditMovieViewModel
                                         {
                                             Id = movieId,
                                             Directors = "Directors",
                                             GenreId = 1,
                                             Name = "Name",
                                             Rating = 3,
                                             Stars = "Stars",
                                             Writers = "Writers"
                                         };

            var result = (RedirectToRouteResult) movieController.Add(editMovieViewModel);

            Assert.IsTrue(string.Compare(result.Action(), "Index", true) == 0);
        }

        [TestMethod]
        public void Delete_WithMovieNotFound_RedirectsToMovieNotFound()
        {
            IMembershipService membershipService = new FakeMembershipService(Enumerable.Empty<MembershipUser>());

            var movies = new List<Movie>();
            IRepository<Movie> movieRepository = new FakeMovieRepository(movies);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            var result = (RedirectToRouteResult)movieController.Edit(42);

            Assert.IsTrue(string.Compare(result.Action(), "MoveNotFound", true) == 0);
        }

        [TestMethod]
        public void Delete_WithMovieFoundNotOwnedByUser_RedirectsToMovieNotAuthorized()
        {
            IMembershipService membershipService = new FakeMembershipService(Enumerable.Empty<MembershipUser>());

            var movies = new List<Movie>();
            const int movieId = 42;
            movies.Add(new Movie { Id = movieId });
            IRepository<Movie> movieRepository = new FakeMovieRepository(movies);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            var result = (RedirectToRouteResult)movieController.Delete(movieId);

            Assert.IsTrue(string.Compare(result.Action(), "MovieNotAuthorized", true) == 0);
        }

        [TestMethod]
        public void Delete_WithMovieFoundOwnedByUser_ReturnsView()
        {
            Guid guid = Guid.NewGuid();
            var users = new List<MembershipUser>();
            users.Add(new TestMembershipUser("test", guid));
            IMembershipService membershipService = new FakeMembershipService(users);

            var movies = new List<Movie>();
            const int movieId = 42;
            movies.Add(new Movie
            {
                Id = movieId,
                aspnet_UsersUserId = guid
            });

            IRepository<Movie> movieRepository = new FakeMovieRepository(movies);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            var result = movieController.Delete(movieId) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete_WithValidMovie_CallsAddOnMovieRepository()
        {
            var users = new List<MembershipUser>();
            Guid guid = Guid.NewGuid();
            users.Add(new TestMembershipUser("test", guid));

            IMembershipService membershipService = new FakeMembershipService(users);

            var movies = new List<Movie>();
            const int movieId = 42;
            movies.Add(new Movie
            {
                Id = movieId,
                aspnet_UsersUserId = guid
            });

            bool wasCalled = false;
            Action deleteCallback = () => wasCalled = true;

            IRepository<Movie> movieRepository = new FakeMovieRepository(movies, deleteCallback: deleteCallback);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            var movie = new Movie
            {
                Id = movieId,
                Directors = "Directors",
                GenreId = 1,
                Name = "Name",
                Rating = 3,
                Stars = "Stars",
                Writers = "Writers"
            };

            movieController.Delete(movie);

            Assert.IsTrue(wasCalled);
        }

        [TestMethod]
        public void Delete_WithValidMovie_CallsSaveOnMovieRepository()
        {
            var users = new List<MembershipUser>();
            Guid guid = Guid.NewGuid();
            users.Add(new TestMembershipUser("test", guid));

            IMembershipService membershipService = new FakeMembershipService(users);

            var movies = new List<Movie>();
            const int movieId = 42;
            movies.Add(new Movie
            {
                Id = movieId,
                aspnet_UsersUserId = guid
            });

            bool wasCalled = false;
            Action saveAction = () => wasCalled = true;

            IRepository<Movie> movieRepository = new FakeMovieRepository(movies, saveCallback: saveAction);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            var movie = new Movie
            {
                Id = movieId,
                Directors = "Directors",
                GenreId = 1,
                Name = "Name",
                Rating = 3,
                Stars = "Stars",
                Writers = "Writers"
            };

            movieController.Delete(movie);

            Assert.IsTrue(wasCalled);
        }

        [TestMethod]
        public void ClearRating_WithValidMovieId_ClearsRatingOnMovie()
        {
            var users = new List<MembershipUser>();
            Guid guid = Guid.NewGuid();
            users.Add(new TestMembershipUser("test", guid));

            IMembershipService membershipService = new FakeMembershipService(users);

            var movies = new List<Movie>();
            const int movieId = 42;
            var movie = new Movie
            {
                aspnet_UsersUserId = guid,
                Id = movieId,
                Directors = "Directors",
                GenreId = 1,
                Name = "Name",
                Rating = 3,
                Stars = "Stars",
                Writers = "Writers"
            };

            movies.Add(movie);
            
            IRepository<Movie> movieRepository = new FakeMovieRepository(movies);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            movieController.ClearRating(movieId);

            Assert.IsTrue(movie.Rating == 0);
        }

        [TestMethod]
        public void ClearRating_WithValidMovieNotOwnedByUser_ReturnsForbiddenResult()
        {
            IMembershipService membershipService = new FakeMembershipService(Enumerable.Empty<MembershipUser>());

            var movies = new List<Movie>();
            const int movieId = 42;
            var movie = new Movie
            {
                Id = movieId,
                Directors = "Directors",
                GenreId = 1,
                Name = "Name",
                Rating = 3,
                Stars = "Stars",
                Writers = "Writers"
            };

            movies.Add(movie);

            IRepository<Movie> movieRepository = new FakeMovieRepository(movies);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            HttpStatusCodeResult result = movieController.ClearRating(movieId) as HttpStatusCodeResult;

            Assert.AreEqual(403, result.StatusCode);
        }

        [TestMethod]
        public void ClearRating_WithValidMovieId_CallsSaveOnMovieRepository()
        {
            var users = new List<MembershipUser>();
            Guid guid = Guid.NewGuid();
            users.Add(new TestMembershipUser("test", guid));

            IMembershipService membershipService = new FakeMembershipService(users);

            var movies = new List<Movie>();
            const int movieId = 42;
            var movie = new Movie
            {
                aspnet_UsersUserId = guid,
                Id = movieId,
                Directors = "Directors",
                GenreId = 1,
                Name = "Name",
                Rating = 3,
                Stars = "Stars",
                Writers = "Writers"
            };

            movies.Add(movie);

            bool wasCalled = false;
            Action saveAction = () => wasCalled = true;

            IRepository<Movie> movieRepository = new FakeMovieRepository(movies, saveCallback: saveAction);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            movieController.ClearRating(movieId);

            Assert.IsTrue(wasCalled);
        }

        [TestMethod]
        public void AddRating_WithValidMovieId_AdjustsMovieRating()
        {
            var users = new List<MembershipUser>();
            Guid guid = Guid.NewGuid();
            users.Add(new TestMembershipUser("test", guid));

            IMembershipService membershipService = new FakeMembershipService(users);

            var movies = new List<Movie>();
            const int movieId = 42;
            var movie = new Movie
            {
                aspnet_UsersUserId = guid,
                Id = movieId,
                Directors = "Directors",
                GenreId = 1,
                Name = "Name",
                Rating = 3,
                Stars = "Stars",
                Writers = "Writers"
            };

            movies.Add(movie);

            IRepository<Movie> movieRepository = new FakeMovieRepository(movies);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            const short newRating = 5;
            movieController.AddRating(movieId, newRating);

            Assert.IsTrue(movie.Rating == newRating);
        }

        [TestMethod]
        public void AddRating_WithValidMovieNotOwnedByUser_ReturnsForbidden()
        {
            IMembershipService membershipService = new FakeMembershipService(Enumerable.Empty<MembershipUser>());

            var movies = new List<Movie>();
            const int movieId = 42;
            var movie = new Movie
            {
                Id = movieId,
                Directors = "Directors",
                GenreId = 1,
                Name = "Name",
                Rating = 3,
                Stars = "Stars",
                Writers = "Writers"
            };

            movies.Add(movie);

            IRepository<Movie> movieRepository = new FakeMovieRepository(movies);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            const short newRating = 5;
            HttpStatusCodeResult result = movieController.AddRating(movieId, newRating) as HttpStatusCodeResult;

            Assert.AreEqual(403, result.StatusCode);
        }

        [TestMethod]
        public void AddRating_WithValidMovieId_CallsSaveOnMovieRepository()
        {
            var users = new List<MembershipUser>();
            Guid guid = Guid.NewGuid();
            users.Add(new TestMembershipUser("test", guid));

            IMembershipService membershipService = new FakeMembershipService(users);

            var movies = new List<Movie>();
            const int movieId = 42;
            var movie = new Movie
            {
                aspnet_UsersUserId = guid,
                Id = movieId,
                Directors = "Directors",
                GenreId = 1,
                Name = "Name",
                Rating = 3,
                Stars = "Stars",
                Writers = "Writers"
            };

            movies.Add(movie);

            bool wasCalled = false;
            Action saveAction = () => wasCalled = true;

            IRepository<Movie> movieRepository = new FakeMovieRepository(movies, saveCallback: saveAction);
            IRepository<Genre> genreRepository = new FakeGenreRepository();
            var movieController = new MovieController(movieRepository, genreRepository, membershipService)
            {
                ControllerContext = new FakeControllerContext()
            };

            movieController.AddRating(movieId, 5);

            Assert.IsTrue(wasCalled);
        }

        [TestMethod]
        public void MoveNotFound_ReturnsView()
        {
            var movieController = new MovieController(null, null, null);

            var result = movieController.MoveNotFound() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}