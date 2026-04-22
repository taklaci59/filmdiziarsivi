using FluentValidation;

namespace filmdiziarsivi.Models
{
    public class MovieValidator : AbstractValidator<Movie>
    {
        public MovieValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş bırakılamaz.")
                .Length(2, 100).WithMessage("Başlık 2 ile 100 karakter arasında olmalıdır.");

            RuleFor(x => x.ReleaseYear)
                .InclusiveBetween(1888, 2100).WithMessage("Geçerli bir çıkış yılı giriniz.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(0, 10).WithMessage("Puan 0 ile 10 arasında olmalıdır.");

            RuleFor(x => x.Views)
                .GreaterThanOrEqualTo(0).WithMessage("İzlenme sayısı negatif olamaz.");

            RuleFor(x => x.TrailerUrl)
                .NotEmpty().WithMessage("Fragman URL gereklidir.")
                .Matches(@"^(http|https)://(www\.)?(youtube\.com/embed/|vimeo\.com/)").WithMessage("Sadece geçerli iframe URL'leri (Youtube embed veya Vimeo) giriniz.");

            RuleFor(x => x.GenreId)
                .GreaterThan(0).WithMessage("Geçerli bir tür seçiniz.");
        }
    }
}
