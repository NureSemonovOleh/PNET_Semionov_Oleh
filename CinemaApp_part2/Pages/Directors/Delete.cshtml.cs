using CinemaApp.Data;
using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaApp.Pages.Directors;

public class DeleteModel : PageModel
{
    private readonly CinemaDbContext _db;
    public DeleteModel(CinemaDbContext db) => _db = db;

    public Director Director { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var d = await _db.Directors.FindAsync(id);
        if (d is null) return NotFound();
        Director = d;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var d = await _db.Directors.FindAsync(id);
        if (d is not null) { _db.Directors.Remove(d); await _db.SaveChangesAsync(); }
        return RedirectToPage("Index");
    }
}
