using CinemaApp.Data;
using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaApp.Pages.Genres;

public class DeleteModel : PageModel
{
    private readonly CinemaDbContext _db;
    public DeleteModel(CinemaDbContext db) => _db = db;

    public Genre Genre { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var g = await _db.Genres.FindAsync(id);
        if (g is null) return NotFound();
        Genre = g;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var g = await _db.Genres.FindAsync(id);
        if (g is not null) { _db.Genres.Remove(g); await _db.SaveChangesAsync(); }
        return RedirectToPage("Index");
    }
}
