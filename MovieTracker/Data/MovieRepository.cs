using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTracker.Data
{
    public class MovieRepository : IRepository<Movie>
    {
        private readonly movietrackerEntities _entities;

        public MovieRepository()
        {
            _entities = new movietrackerEntities();
        }

        #region IRepository<Movie> Members

        public void Save()
        {
            _entities.SaveChanges();
        }

        public IEnumerable<Movie> Find(Func<Movie, bool> filter)
        {
            return _entities.Movies.Where(filter);
        }

        public void Delete(Movie movie)
        {
            _entities.Movies.DeleteObject(movie);
        }

        public Movie Add(Movie model)
        {
            _entities.Movies.AddObject(model);
            return model;
        }

        public IEnumerable<Movie> GetAll()
        {
            return _entities.Movies;
        }

        #endregion
    }
}