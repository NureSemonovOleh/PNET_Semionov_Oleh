using CinemaApp.Models;
using CinemaApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaApp.Pages.Sessions;

public class IndexModel : PageModel
{
    private readonly ISessionService _sessionService;

    public IndexModel(ISessionService sessionService) => _sessionService = sessionService;

    public List<Session> Sessions { get; set; } = new();

    public async Task OnGetAsync() =>
        Sessions = await _sessionService.GetAllAsync();
}
