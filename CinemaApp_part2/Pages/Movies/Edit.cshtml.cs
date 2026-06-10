using CinemaApp.Data;
using CinemaApp.Models;
using CinemaApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Pages.Movies;

public class EditModel : PageModel
{
    private readonly IMovieService _movieService;
    private readonly CinemaDbContext _db;

    public EditModel(IMovieService movieService, CinemaDbContext db)
    {
        _movieService = movieService;
        _db = db;
    }

    [BindProperty]
    public Movie Movie { get; set; } = default!;

    public SelectList Directors { get; set; } = default!;
    public SelectList Genres { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var movie = await _movieService.GetByIdAsync(id);
        if (movie is null) return NotFound();
        Movie = movie;
        await LoadSelectListsAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadSelectListsAsync();
            return Page();
        }

        await _movieService.UpdateAsync(Movie);
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
