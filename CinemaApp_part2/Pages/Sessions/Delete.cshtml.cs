using CinemaApp.Models;
using CinemaApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaApp.Pages.Sessions;

public class DeleteModel : PageModel
{
    private readonly ISessionService _sessionService;

    public DeleteModel(ISessionService sessionService) => _sessionService = sessionService;

    public Session Session { get; set; } = default!;
    public string? ErrorMessage { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var session = await _sessionService.GetByIdAsync(id);
        if (session is null) return NotFound();
        Session = session;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        try
        {
            await _sessionService.DeleteAsync(id);
            return RedirectToPage("Index");
        }
        catch (InvalidOperationException ex)
        {
            var session = await _sessionService.GetByIdAsync(id);
            if (session is null) return NotFound();
            Session = session;
            ErrorMessage = ex.Message;
            return Page();
        }
    }
}
