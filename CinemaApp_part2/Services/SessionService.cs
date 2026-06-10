using CinemaApp.Data;
using CinemaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services;

public class SessionService : ISessionService
{
    private readonly CinemaDbContext _db;
    private readonly ILogger<SessionService> _logger;

    public SessionService(CinemaDbContext db, ILogger<SessionService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<List<Session>> GetAllAsync()
    {
        _logger.LogInformation("Fetching all sessions");
        return await _db.Sessions
            .Include(s => s.Movie)
            .Include(s => s.Hall)
            .OrderBy(s => s.SessionDate)
            .ThenBy(s => s.StartTime)
            .ToListAsync();
    }

    public async Task<Session?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Fetching session ID {Id}", id);
        return await _db.Sessions
            .Include(s => s.Movie)
            .Include(s => s.Hall)
            .FirstOrDefaultAsync(s => s.SessionId == id);
    }

    public async Task<Session> CreateAsync(Session session)
    {
        _logger.LogInformation("Creating session for movie ID {MovieId}", session.MovieId);
        _db.Sessions.Add(session);
        await _db.SaveChangesAsync();
        _logger.LogInformation("Session created with ID {Id}", session.SessionId);
        return session;
    }

    public async Task<Session> UpdateAsync(Session session)
    {
        _logger.LogInformation("Updating session ID {Id}", session.SessionId);
        var old = await _db.Sessions.AsNoTracking()
            .FirstOrDefaultAsync(s => s.SessionId == session.SessionId);

        _db.Sessions.Update(session);
        await _db.SaveChangesAsync();

        if (old != null && old.TicketsSold != session.TicketsSold)
        {
            _db.SessionLogs.Add(new SessionLog
            {
                SessionId = session.SessionId,
                ModifyDate = DateTime.Now,
                OldTicketsSold = old.TicketsSold,
                NewTicketsSold = session.TicketsSold,
                Operation = "UPDATE"
            });
            await _db.SaveChangesAsync();
        }

        return session;
    }

    public async Task DeleteAsync(int id)
    {
        _logger.LogInformation("Deleting session ID {Id}", id);
        var session = await _db.Sessions.FindAsync(id);
        if (session is null) return;

        var movieSessionCount = await _db.Sessions
            .CountAsync(s => s.MovieId == session.MovieId);

        if (movieSessionCount <= 1)
        {
            _logger.LogWarning("Cannot delete last session for movie ID {MovieId}", session.MovieId);
            throw new InvalidOperationException("Неможливо видалити останній сеанс фільму.");
        }

        _db.Sessions.Remove(session);
        await _db.SaveChangesAsync();
        _logger.LogInformation("Session ID {Id} deleted", id);
    }

    public async Task<bool> SellTicketsAsync(int sessionId, int count)
    {
        var session = await _db.Sessions
            .Include(s => s.Hall)
            .FirstOrDefaultAsync(s => s.SessionId == sessionId);

        if (session is null) return false;

        if (session.TicketsSold + count > session.Hall!.Capacity)
        {
            _logger.LogWarning("Overbooking attempt on session ID {Id}", sessionId);
            return false;
        }

        var old = session.TicketsSold;
        session.TicketsSold += count;
        _db.SessionLogs.Add(new SessionLog
        {
            SessionId = sessionId,
            ModifyDate = DateTime.Now,
            OldTicketsSold = old,
            NewTicketsSold = session.TicketsSold,
            Operation = "SELL"
        });

        await _db.SaveChangesAsync();
        _logger.LogInformation("Sold {Count} tickets for session ID {Id}", count, sessionId);
        return true;
    }
}
