using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using filmdiziarsivi.Models;
using filmdiziarsivi.Services;
using System.Linq;

namespace filmdiziarsivi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        private readonly IExportService _exportService;

        public HomeController(ILogger<HomeController> logger, IMovieService movieService, IExportService exportService)
        {
            _logger = logger;
            _movieService = movieService;
            _exportService = exportService;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return View(movies);
        }

        [HttpGet]
        public async Task<IActionResult> GetChartData()
        {
            var topRated = await _movieService.GetTopRatedMoviesAsync(5);
            var mostWatched = await _movieService.GetMostWatchedMoviesAsync(5);

            return Json(new
            {
                topRated = new
                {
                    labels = topRated.Select(m => m.Title).ToArray(),
                    data = topRated.Select(m => m.Rating).ToArray()
                },
                mostWatched = new
                {
                    labels = mostWatched.Select(m => m.Title).ToArray(),
                    data = mostWatched.Select(m => m.Views).ToArray()
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> ExportExcel()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            var content = await _exportService.ExportMoviesToExcelAsync(movies);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Arsiv.xlsx");
        }

        [HttpGet]
        public async Task<IActionResult> ExportPdf()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            var content = await _exportService.ExportMoviesToPdfAsync(movies);
            return File(content, "application/pdf", "Arsiv.pdf");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
