using CinemaApp.Models;

namespace CinemaApp.Services;

public interface ISessionService
{
    Task<List<Session>> GetAllAsync();
    Task<Session?> GetByIdAsync(int id);
    Task<Session> CreateAsync(Session session);
    Task<Session> UpdateAsync(Session session);
    Task DeleteAsync(int id);
    Task<bool> SellTicketsAsync(int sessionId, int count);
}
