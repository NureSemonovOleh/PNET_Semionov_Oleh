using CinemaApp.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Pages;

public class IndexModel : PageModel
{
    private readonly CinemaDbContext _db;

    public IndexModel(CinemaDbContext db) => _db = db;

    public int MovieCount { get; set; }
    public int SessionCount { get; set; }
    public int DirectorCount { get; set; }

    public async Task OnGetAsync()
    {
        MovieCount = await _db.Movies.CountAsync();
        SessionCount = await _db.Sessions.CountAsync();
        DirectorCount = await _db.Directors.CountAsync();
    }
}
