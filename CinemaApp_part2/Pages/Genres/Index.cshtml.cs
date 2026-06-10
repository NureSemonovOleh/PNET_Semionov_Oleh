using CinemaApp.Data;
using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Pages.Genres;

public class IndexModel : PageModel
{
    private readonly CinemaDbContext _db;
    public IndexModel(CinemaDbContext db) => _db = db;

    public List<Genre> Genres { get; set; } = new();

    public async Task OnGetAsync() =>
        Genres = await _db.Genres.Include(g => g.Movies).OrderBy(g => g.Name).ToListAsync();
}
