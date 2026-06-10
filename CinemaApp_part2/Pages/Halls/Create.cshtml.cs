using CinemaApp.Data;
using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaApp.Pages.Halls;

public class CreateModel : PageModel
{
    private readonly CinemaDbContext _db;
    public CreateModel(CinemaDbContext db) => _db = db;

    [BindProperty] public Hall Hall { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        _db.Halls.Add(Hall);
        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
