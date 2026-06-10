using CinemaApp.Models;
using CinemaApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaApp.Pages.Movies;

public class IndexModel : PageModel
{
    private readonly IMovieService _movieService;

    public IndexModel(IMovieService movieService) => _movieService = movieService;

    public List<Movie> Movies { get; set; } = new();

    public async Task OnGetAsync() =>
        Movies = await _movieService.GetAllAsync();
}
