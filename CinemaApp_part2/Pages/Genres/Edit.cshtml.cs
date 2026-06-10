using CinemaApp.Data;
using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaApp.Pages.Genres;

public class EditModel : PageModel
{
    private readonly CinemaDbContext _db;
    public EditModel(CinemaDbContext db) => _db = db;

    [BindProperty] public Genre Genre { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var g = await _db.Genres.FindAsync(id);
        if (g is null) return NotFound();
        Genre = g;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        _db.Genres.Update(Genre);
        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
