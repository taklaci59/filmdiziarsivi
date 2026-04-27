using System.ComponentModel.DataAnnotations;

namespace filmdiziarsivi.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Display(Name = "Başlık")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Çıkış Yılı")]
        public int ReleaseYear { get; set; }

        [Display(Name = "Puan")]
        public double Rating { get; set; }

        [Display(Name = "İzlenme")]
        public int Views { get; set; }

        [Display(Name = "Fragman URL")]
        public string TrailerUrl { get; set; } = string.Empty;

        [Display(Name = "Tür")]
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
