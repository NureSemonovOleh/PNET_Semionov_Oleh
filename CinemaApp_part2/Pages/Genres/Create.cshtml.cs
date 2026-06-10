using CinemaApp.Data;
using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaApp.Pages.Genres;

public class CreateModel : PageModel
{
    private readonly CinemaDbContext _db;
    public CreateModel(CinemaDbContext db) => _db = db;

    [BindProperty] public Genre Genre { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        _db.Genres.Add(Genre);
        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
