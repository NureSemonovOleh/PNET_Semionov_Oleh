using CinemaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data;

public class CinemaDbContext : DbContext
{
    public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options) { }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Hall> Halls { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<SessionLog> SessionLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Genre>(e =>
        {
            e.HasKey(g => g.GenreId);
            e.Property(g => g.Name).HasMaxLength(30).IsRequired();
            e.Property(g => g.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<Director>(e =>
        {
            e.HasKey(d => d.DirectorId);
            e.Property(d => d.LastName).HasMaxLength(30).IsRequired();
            e.Property(d => d.FirstName).HasMaxLength(30).IsRequired();
            e.Property(d => d.Country).HasMaxLength(30);
            e.Ignore(d => d.FullName);
        });

        modelBuilder.Entity<Movie>(e =>
        {
            e.HasKey(m => m.MovieId);
            e.Property(m => m.Title).HasMaxLength(100).IsRequired();
            e.Property(m => m.Description).HasMaxLength(500);
            e.HasOne(m => m.Director)
                .WithMany(d => d.Movies)
                .HasForeignKey(m => m.DirectorId)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(m => m.Genre)
                .WithMany(g => g.Movies)
                .HasForeignKey(m => m.GenreId)
                .OnDelete(DeleteBehavior.Restrict);
            e.Ignore(m => m.Sessions);
        });

        modelBuilder.Entity<Hall>(e =>
        {
            e.HasKey(h => h.HallId);
            e.Property(h => h.Name).HasMaxLength(30).IsRequired();
            e.Property(h => h.HallType).HasMaxLength(20);
        });

        modelBuilder.Entity<Session>(e =>
        {
            e.HasKey(s => s.SessionId);
            e.Property(s => s.TicketPrice).HasColumnType("float");
            e.HasOne(s => s.Movie)
                .WithMany(m => m.Sessions)
                .HasForeignKey(s => s.MovieId)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(s => s.Hall)
                .WithMany(h => h.Sessions)
                .HasForeignKey(s => s.HallId)
                .OnDelete(DeleteBehavior.Restrict);
            e.Ignore(s => s.Revenue);
        });

        modelBuilder.Entity<SessionLog>(e =>
        {
            e.HasKey(l => l.LogId);
            e.Property(l => l.Operation).HasMaxLength(10);
            e.HasOne(l => l.Session)
                .WithMany(s => s.SessionLogs)
                .HasForeignKey(l => l.SessionId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
