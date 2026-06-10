using CinemaApp.Models;
using CinemaApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaApp.Pages.Movies;

public class DeleteModel : PageModel
{
    private readonly IMovieService _movieService;

    public DeleteModel(IMovieService movieService) => _movieService = movieService;

    public Movie Movie { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var movie = await _movieService.GetByIdAsync(id);
        if (movie is null) return NotFound();
        Movie = movie;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        await _movieService.DeleteAsync(id);
        return RedirectToPage("Index");
    }
}
