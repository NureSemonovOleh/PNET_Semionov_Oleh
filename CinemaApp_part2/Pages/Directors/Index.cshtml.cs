using CinemaApp.Data;
using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Pages.Directors;

public class IndexModel : PageModel
{
    private readonly CinemaDbContext _db;
    public IndexModel(CinemaDbContext db) => _db = db;

    public List<Director> Directors { get; set; } = new();

    public async Task OnGetAsync() =>
        Directors = await _db.Directors.Include(d => d.Movies).OrderBy(d => d.LastName).ToListAsync();
}
