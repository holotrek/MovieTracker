using System;
using System.Collections.Generic;
using System.Linq;
using MovieTracker.Data;

namespace MovieTracker.Tests.Unit.Fakes
{
    public class FakeGenreRepository : IRepository<Genre>
    {
        private readonly IEnumerable<Genre> _genres;

        public FakeGenreRepository():this(Enumerable.Empty<Genre>())
        {
            
        }

        public FakeGenreRepository(IEnumerable<Genre> genres)
        {
            _genres = genres;
        }
        public void Save()
        {
        }

        public IEnumerable<Genre> Find(Func<Genre, bool> filter)
        {
            return _genres.Where(filter);
        }

        public void Delete(Genre model)
        {
            
        }

        public Genre Add(Genre model)
        {
            return model;
        }

        public IEnumerable<Genre> GetAll()
        {
            return _genres;
        }
    }
}