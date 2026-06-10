using CinemaApp.Data;
using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Pages.Halls;

public class IndexModel : PageModel
{
    private readonly CinemaDbContext _db;
    public IndexModel(CinemaDbContext db) => _db = db;

    public List<Hall> Halls { get; set; } = new();

    public async Task OnGetAsync() =>
        Halls = await _db.Halls.OrderBy(h => h.Name).ToListAsync();
}
