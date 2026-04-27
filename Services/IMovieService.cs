using System.Collections.Generic;
using System.Threading.Tasks;
using filmdiziarsivi.Models;

namespace filmdiziarsivi.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<IEnumerable<Movie>> GetTopRatedMoviesAsync(int count = 5);
        Task<IEnumerable<Movie>> GetMostWatchedMoviesAsync(int count = 5);
        Task<Movie?> GetMovieByIdAsync(int id);
        Task<Movie> AddMovieAsync(Movie movie);
        Task UpdateMovieAsync(Movie movie);
        Task DeleteMovieAsync(int id);
    }
}
