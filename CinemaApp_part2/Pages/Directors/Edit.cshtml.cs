using CinemaApp.Data;
using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaApp.Pages.Directors;

public class EditModel : PageModel
{
    private readonly CinemaDbContext _db;
    public EditModel(CinemaDbContext db) => _db = db;

    [BindProperty] public Director Director { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var d = await _db.Directors.FindAsync(id);
        if (d is null) return NotFound();
        Director = d;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        _db.Directors.Update(Director);
        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
