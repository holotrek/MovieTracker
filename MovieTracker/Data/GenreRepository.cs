using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTracker.Data
{
    public class GenreRepository : IRepository<Genre>
    {
        private readonly movietrackerEntities _entities;

        public GenreRepository()
        {
            _entities = new movietrackerEntities();
        }

        #region IRepository<Genre> Members

        public void Save()
        {
            _entities.SaveChanges();
        }

        public IEnumerable<Genre> Find(Func<Genre, bool> filter)
        {
            return _entities.Genres.Where(filter);
        }

        public void Delete(Genre genre)
        {
            _entities.Genres.DeleteObject(genre);
            _entities.SaveChanges();
        }

        public Genre Add(Genre model)
        {
            _entities.Genres.AddObject(model);
            _entities.SaveChanges();

            return model;
        }

        public IEnumerable<Genre> GetAll()
        {
            return _entities.Genres;
        }

        #endregion
    }
}