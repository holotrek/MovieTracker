using System.ComponentModel.DataAnnotations;

namespace MovieTracker.Models
{
    public class MovieMetaData
    {

        [Display(Name = "Title")]
        public string Name { get; set; }

        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        [Display(Name = "Rating 1-5")]
        public short Rating { get; set; }

        [Display(Name = "Directors")]
        public string Directors { get; set; }

        [Display(Name = "Writers")]
        public string Writers { get; set; }

        [Display(Name = "Stars/Actors")]
        public string Stars { get; set; }
    }
}