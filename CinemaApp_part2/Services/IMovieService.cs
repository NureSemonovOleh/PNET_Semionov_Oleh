using CinemaApp.Models;

namespace CinemaApp.Services;

public interface IMovieService
{
    Task<List<Movie>> GetAllAsync();
    Task<Movie?> GetByIdAsync(int id);
    Task<Movie> CreateAsync(Movie movie);
    Task<Movie> UpdateAsync(Movie movie);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
