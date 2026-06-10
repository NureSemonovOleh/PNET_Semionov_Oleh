using CinemaApp.Data;
using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaApp.Pages.Halls;

public class DeleteModel : PageModel
{
    private readonly CinemaDbContext _db;
    public DeleteModel(CinemaDbContext db) => _db = db;

    public Hall Hall { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var h = await _db.Halls.FindAsync(id);
        if (h is null) return NotFound();
        Hall = h;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var h = await _db.Halls.FindAsync(id);
        if (h is not null) { _db.Halls.Remove(h); await _db.SaveChangesAsync(); }
        return RedirectToPage("Index");
    }
}
