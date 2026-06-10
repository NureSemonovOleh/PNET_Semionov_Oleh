using CinemaApp.Data;
using CinemaApp.Models;
using CinemaApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Pages.Sessions;

public class CreateModel : PageModel
{
    private readonly ISessionService _sessionService;
    private readonly CinemaDbContext _db;

    public CreateModel(ISessionService sessionService, CinemaDbContext db)
    {
        _sessionService = sessionService;
        _db = db;
    }

    [BindProperty]
    public Session Session { get; set; } = new() { SessionDate = DateOnly.FromDateTime(DateTime.Today) };

    public SelectList Movies { get; set; } = default!;
    public SelectList Halls { get; set; } = default!;

    public async Task OnGetAsync() => await LoadSelectListsAsync();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadSelectListsAsync();
            return Page();
        }

        await _sessionService.CreateAsync(Session);
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
