using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using filmdiziarsivi.Models;

namespace filmdiziarsivi.Services
{
    public class ExportService : IExportService
    {
        public ExportService()
        {
            // QuestPDF Community License requirement
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public async Task<byte[]> ExportMoviesToExcelAsync(IEnumerable<Movie> movies)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Filmler");

            // Header
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Başlık";
            worksheet.Cell(1, 3).Value = "Çıkış Yılı";
            worksheet.Cell(1, 4).Value = "Puan";
            worksheet.Cell(1, 5).Value = "İzlenme";
            worksheet.Cell(1, 6).Value = "Tür";

            var range = worksheet.Range("A1:F1");
            range.Style.Font.Bold = true;
            range.Style.Fill.BackgroundColor = XLColor.LightGray;

            // Data
            int row = 2;
            foreach (var movie in movies)
            {
                worksheet.Cell(row, 1).Value = movie.Id;
                worksheet.Cell(row, 2).Value = movie.Title;
                worksheet.Cell(row, 3).Value = movie.ReleaseYear;
                worksheet.Cell(row, 4).Value = movie.Rating;
                worksheet.Cell(row, 5).Value = movie.Views;
                worksheet.Cell(row, 6).Value = movie.Genre?.Name ?? "Bilinmiyor";
                row++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return await Task.FromResult(stream.ToArray());
        }

        public async Task<byte[]> ExportMoviesToPdfAsync(IEnumerable<Movie> movies)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Text("Film ve Dizi Arşivi").SemiBold().FontSize(24).FontColor(Colors.Blue.Darken2);

                    page.Content().PaddingVertical(1, Unit.Centimetre).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(2);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Başlık").SemiBold();
                            header.Cell().Text("Çıkış").SemiBold();
                            header.Cell().Text("Puan").SemiBold();
                            header.Cell().Text("İzlenme").SemiBold();
                            header.Cell().Text("Tür").SemiBold();
                            header.Cell().PaddingBottom(5).BorderBottom(1).BorderColor(Colors.Black);
                        });

                        foreach (var movie in movies)
                        {
                            table.Cell().Text(movie.Title);
                            table.Cell().Text(movie.ReleaseYear.ToString());
                            table.Cell().Text(movie.Rating.ToString());
                            table.Cell().Text(movie.Views.ToString());
                            table.Cell().Text(movie.Genre?.Name ?? "");
                        }
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Sayfa ");
                            x.CurrentPageNumber();
                        });
                });
            });

            using var memoryStream = new MemoryStream();
            document.GeneratePdf(memoryStream);
            return await Task.FromResult(memoryStream.ToArray());
        }
    }
}
