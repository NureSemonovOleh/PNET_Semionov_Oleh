using CinemaApp.Data;
using CinemaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services;

public class MovieService : IMovieService
{
    private readonly CinemaDbContext _db;
    private readonly ILogger<MovieService> _logger;

    public MovieService(CinemaDbContext db, ILogger<MovieService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<List<Movie>> GetAllAsync()
    {
        _logger.LogInformation("Fetching all movies");
        return await _db.Movies
            .Include(m => m.Director)
            .Include(m => m.Genre)
            .OrderBy(m => m.Title)
            .ToListAsync();
    }

    public async Task<Movie?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Fetching movie with ID {Id}", id);
        return await _db.Movies
            .Include(m => m.Director)
            .Include(m => m.Genre)
            .FirstOrDefaultAsync(m => m.MovieId == id);
    }

    public async Task<Movie> CreateAsync(Movie movie)
    {
        _logger.LogInformation("Creating movie '{Title}'", movie.Title);
        _db.Movies.Add(movie);
        await _db.SaveChangesAsync();
        _logger.LogInformation("Movie created with ID {Id}", movie.MovieId);
        return movie;
    }

    public async Task<Movie> UpdateAsync(Movie movie)
    {
        _logger.LogInformation("Updating movie ID {Id}", movie.MovieId);
        _db.Movies.Update(movie);
        await _db.SaveChangesAsync();
        return movie;
    }

    public async Task DeleteAsync(int id)
    {
        _logger.LogInformation("Deleting movie ID {Id}", id);
        var movie = await _db.Movies.FindAsync(id);
        if (movie is null)
        {
            _logger.LogWarning("Movie ID {Id} not found for deletion", id);
            return;
        }
        _db.Movies.Remove(movie);
        await _db.SaveChangesAsync();
        _logger.LogInformation("Movie ID {Id} deleted", id);
    }

    public async Task<bool> ExistsAsync(int id) =>
        await _db.Movies.AnyAsync(m => m.MovieId == id);
}
