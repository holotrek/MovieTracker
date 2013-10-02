using System.ComponentModel.DataAnnotations;
using MovieTracker.Models;

namespace MovieTracker.Data
{
    [MetadataType(typeof(MovieMetaData))]
    public partial class Movie
    {
        public void ClearRating()
        {
            Rating = 0;
        }
    }
}