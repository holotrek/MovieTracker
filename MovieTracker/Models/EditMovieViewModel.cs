using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace MovieTracker.Models
{
    public class EditMovieViewModel
    {
        public EditMovieViewModel()
        {
            var ratings = Enumerable.Range(0, 6).Select(r => new SelectListItem {Text = string.Format("{0} Star", r), Value = r.ToString()});

            RatingOptions = ratings;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        public SelectList Genres { get; set; }

        [Required]
        [Range(0, 5)]
        [Display(Name = "Rating 1-5")]
        public short Rating { get; set; }

        [StringLength(200)]
        [Display(Name = "Directors")]
        public string Directors { get; set; }

        [StringLength(200)]
        [Display(Name = "Writers")]
        public string Writers { get; set; }

        [StringLength(200)]
        [Display(Name = "Stars/Actors")]
        public string Stars { get; set; }

        public IEnumerable<SelectListItem> RatingOptions { get; private set; }
    }
}