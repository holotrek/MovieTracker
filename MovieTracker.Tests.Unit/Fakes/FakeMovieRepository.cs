using System;
using System.Collections.Generic;
using System.Linq;
using MovieTracker.Data;

namespace MovieTracker.Tests.Unit.Fakes
{
    public class FakeMovieRepository : IRepository<Movie>
    {
        private readonly IEnumerable<Movie> _movies;
        private readonly Action _addCallback;
        private readonly Action _saveCallback;
        private readonly Action _deleteCallback;

        public FakeMovieRepository():this(Enumerable.Empty<Movie>())
        {
            
        }

        public FakeMovieRepository(IEnumerable<Movie> movies)
        {
            _movies = movies;
        }

        public FakeMovieRepository(IEnumerable<Movie> movies, Action addCallback = null, Action saveCallback = null, Action deleteCallback = null)
        {
            _movies = movies;
            _addCallback = addCallback;
            _saveCallback = saveCallback;
            _deleteCallback = deleteCallback;
        }

        public void Save()
        {
            if(_saveCallback != null)
                _saveCallback();
        }

        public IEnumerable<Movie> Find(Func<Movie, bool> filter)
        {
            return _movies.Where(filter);
        }

        public void Delete(Movie model)
        {
            if(_deleteCallback != null)
                _deleteCallback();
        }

        public Movie Add(Movie model)
        {
            if(_addCallback != null)
                _addCallback();

            return model;
        }

        public IEnumerable<Movie> GetAll()
        {
            return _movies;
        }
    }
}