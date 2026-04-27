using Microsoft.EntityFrameworkCore;
using filmdiziarsivi.Models;

namespace filmdiziarsivi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data for testing
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Aksiyon" },
                new Genre { Id = 2, Name = "Bilim Kurgu" },
                new Genre { Id = 3, Name = "Dram" },
                new Genre { Id = 4, Name = "Komedi" }
            );

            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Matrix", ReleaseYear = 1999, Rating = 8.7, Views = 1500, TrailerUrl = "https://www.youtube.com/embed/vKQi3bBA1y8", GenreId = 2 },
                new Movie { Id = 2, Title = "Inception", ReleaseYear = 2010, Rating = 8.8, Views = 2000, TrailerUrl = "https://www.youtube.com/embed/YoHD9XEInc0", GenreId = 2 },
                new Movie { Id = 3, Title = "The Dark Knight", ReleaseYear = 2008, Rating = 9.0, Views = 2500, TrailerUrl = "https://www.youtube.com/embed/EXeTwQWrcwY", GenreId = 1 },
                new Movie { Id = 4, Title = "Hababam Sınıfı", ReleaseYear = 1975, Rating = 9.2, Views = 5000, TrailerUrl = "https://www.youtube.com/embed/rGZ4I6s3UeQ", GenreId = 4 },
                new Movie { Id = 5, Title = "Esaretin Bedeli", ReleaseYear = 1994, Rating = 9.3, Views = 3000, TrailerUrl = "https://www.youtube.com/embed/6hB3S9bIaco", GenreId = 3 }
            );
        }
    }
}
