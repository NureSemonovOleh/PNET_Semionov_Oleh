using CinemaApp.Data;
using CinemaApp.Models;
using CinemaApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Pages.Movies;

public class CreateModel : PageModel
{
    private readonly IMovieService _movieService;
    private readonly CinemaDbContext _db;

    public CreateModel(IMovieService movieService, CinemaDbContext db)
    {
        _movieService = movieService;
        _db = db;
    }

    [BindProperty]
    public Movie Movie { get; set; } = new();

    public SelectList Directors { get; set; } = default!;
    public SelectList Genres { get; set; } = default!;

    public async Task OnGetAsync()
    {
        await LoadSelectListsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadSelectListsAsync();
            return Page();
        }

        await _movieService.CreateAsync(Movie);
        return RedirectToPage("Index");
    }

    private async Task LoadSelectListsAsync()
    {
        var directors = await _db.Directors.OrderBy(d => d.LastName).ToListAsync();
        Directors = new SelectList(directors, "DirectorId", "FullName");
        var genres = await _db.Genres.OrderBy(g => g.Name).ToListAsync();
        Genres = new SelectList(genres, "GenreId", "Name");
    }
}
