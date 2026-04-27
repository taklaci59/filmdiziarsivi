using System.Collections.Generic;
using System.Threading.Tasks;
using filmdiziarsivi.Models;

namespace filmdiziarsivi.Services
{
    public interface IExportService
    {
        Task<byte[]> ExportMoviesToExcelAsync(IEnumerable<Movie> movies);
        Task<byte[]> ExportMoviesToPdfAsync(IEnumerable<Movie> movies);
    }
}
