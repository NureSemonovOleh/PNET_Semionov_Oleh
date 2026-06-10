using CinemaApp.Data;
using CinemaApp.Models;
using CinemaApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Pages.Sessions;

public class EditModel : PageModel
{
    private readonly ISessionService _sessionService;
    private readonly CinemaDbContext _db;

    public EditModel(ISessionService sessionService, CinemaDbContext db)
    {
        _sessionService = sessionService;
        _db = db;
    }

    [BindProperty]
    public Session Session { get; set; } = default!;

    public SelectList Movies { get; set; } = default!;
    public SelectList Halls { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var session = await _sessionService.GetByIdAsync(id);
        if (session is null) return NotFound();
        Session = session;
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

        await _sessionService.UpdateAsync(Session);
        return RedirectToPage("Index");
    }

    private async Task LoadSelectListsAsync()
    {
        var movies = await _db.Movies.OrderBy(m => m.Title).ToListAsync();
        Movies = new SelectList(movies, "MovieId", "Title");
        var halls = await _db.Halls.OrderBy(h => h.Name).ToListAsync();
        Halls = new SelectList(halls, "HallId", "Name");
    }
}
