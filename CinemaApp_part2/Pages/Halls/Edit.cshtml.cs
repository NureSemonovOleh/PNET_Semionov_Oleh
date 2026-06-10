using CinemaApp.Data;
using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaApp.Pages.Halls;

public class EditModel : PageModel
{
    private readonly CinemaDbContext _db;
    public EditModel(CinemaDbContext db) => _db = db;

    [BindProperty] public Hall Hall { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var h = await _db.Halls.FindAsync(id);
        if (h is null) return NotFound();
        Hall = h;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        _db.Halls.Update(Hall);
        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
