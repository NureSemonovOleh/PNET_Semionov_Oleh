using CinemaApp.Models;

namespace CinemaApp.Data;

public static class DbSeeder
{
    public static void Seed(CinemaDbContext db)
    {
        if (db.Genres.Any()) return;

        var genres = new List<Genre>
        {
            new() { Name = "Фантастика" },
            new() { Name = "Трилер" },
            new() { Name = "Драма" },
            new() { Name = "Жахи" },
            new() { Name = "Анімація" }
        };
        db.Genres.AddRange(genres);
        db.SaveChanges();

        var directors = new List<Director>
        {
            new() { LastName = "Нолан",    FirstName = "Крістофер", Country = "Великобританія" },
            new() { LastName = "Спілберг", FirstName = "Стівен",    Country = "США" },
            new() { LastName = "Тарантіно",FirstName = "Квентін",   Country = "США" },
            new() { LastName = "Кубрік",   FirstName = "Стенлі",    Country = "США" },
            new() { LastName = "Кемерон",  FirstName = "Джеймс",    Country = "Канада" }
        };
        db.Directors.AddRange(directors);
        db.SaveChanges();

        var halls = new List<Hall>
        {
            new() { Name = "IMAX зал",    Capacity = 200, HallType = "IMAX" },
            new() { Name = "3D зал",      Capacity = 150, HallType = "3D" },
            new() { Name = "Стандарт",    Capacity = 100, HallType = "Standard" }
        };
        db.Halls.AddRange(halls);
        db.SaveChanges();

        int nolan = directors[0].DirectorId, spielberg = directors[1].DirectorId,
            taran = directors[2].DirectorId, kubrick = directors[3].DirectorId,
            cameron = directors[4].DirectorId;
        int scifi = genres[0].GenreId, thriller = genres[1].GenreId,
            drama = genres[2].GenreId, horror = genres[3].GenreId,
            anim = genres[4].GenreId;

        var movies = new List<Movie>
        {
            new() { Title = "Темний Лицар",          DirectorId = nolan,     GenreId = thriller, Year = 2008, Duration = 152, Rating = 9.0 },
            new() { Title = "Початок",               DirectorId = nolan,     GenreId = scifi,    Year = 2010, Duration = 148, Rating = 8.8 },
            new() { Title = "Інтерстеллар",          DirectorId = nolan,     GenreId = scifi,    Year = 2014, Duration = 169, Rating = 8.6 },
            new() { Title = "Список Шиндлера",       DirectorId = spielberg, GenreId = drama,    Year = 1993, Duration = 195, Rating = 9.0 },
            new() { Title = "Інопланетянин",         DirectorId = spielberg, GenreId = scifi,    Year = 1982, Duration = 115, Rating = 7.9 },
            new() { Title = "Кримінальне чтиво",     DirectorId = taran,     GenreId = thriller, Year = 1994, Duration = 154, Rating = 8.9 },
            new() { Title = "Джанго вільний",        DirectorId = taran,     GenreId = drama,    Year = 2012, Duration = 165, Rating = 8.4 },
            new() { Title = "Сяйво",                 DirectorId = kubrick,   GenreId = horror,   Year = 1980, Duration = 146, Rating = 8.4 },
            new() { Title = "2001: Космічна одіссея",DirectorId = kubrick,   GenreId = scifi,    Year = 1968, Duration = 149, Rating = 8.3 },
            new() { Title = "Аватар",                DirectorId = cameron,   GenreId = scifi,    Year = 2009, Duration = 162, Rating = 7.9 },
            new() { Title = "Титанік",               DirectorId = cameron,   GenreId = drama,    Year = 1997, Duration = 194, Rating = 7.9 },
            new() { Title = "Оппенгеймер",           DirectorId = nolan,     GenreId = drama,    Year = 2023, Duration = 180, Rating = 8.5 }
        };
        db.Movies.AddRange(movies);
        db.SaveChanges();

        int imax = halls[0].HallId, hall3d = halls[1].HallId, std = halls[2].HallId;

        var sessions = new List<Session>
        {
            new() { MovieId = movies[0].MovieId,  HallId = imax,  SessionDate = new DateOnly(2026,6,15), StartTime = new TimeOnly(10,0),  TicketPrice = 250, TicketsSold = 180 },
            new() { MovieId = movies[0].MovieId,  HallId = hall3d,SessionDate = new DateOnly(2026,6,15), StartTime = new TimeOnly(14,0),  TicketPrice = 200, TicketsSold = 120 },
            new() { MovieId = movies[2].MovieId,  HallId = imax,  SessionDate = new DateOnly(2026,6,16), StartTime = new TimeOnly(11,0),  TicketPrice = 280, TicketsSold = 180 },
            new() { MovieId = movies[2].MovieId,  HallId = std,   SessionDate = new DateOnly(2026,6,17), StartTime = new TimeOnly(18,0),  TicketPrice = 150, TicketsSold = 90  },
            new() { MovieId = movies[3].MovieId,  HallId = hall3d,SessionDate = new DateOnly(2026,6,15), StartTime = new TimeOnly(16,0),  TicketPrice = 180, TicketsSold = 100 },
            new() { MovieId = movies[5].MovieId,  HallId = std,   SessionDate = new DateOnly(2026,6,16), StartTime = new TimeOnly(20,0),  TicketPrice = 170, TicketsSold = 80  },
            new() { MovieId = movies[7].MovieId,  HallId = hall3d,SessionDate = new DateOnly(2026,6,18), StartTime = new TimeOnly(21,0),  TicketPrice = 160, TicketsSold = 70  },
            new() { MovieId = movies[7].MovieId,  HallId = std,   SessionDate = new DateOnly(2026,6,19), StartTime = new TimeOnly(19,0),  TicketPrice = 150, TicketsSold = 60  },
            new() { MovieId = movies[8].MovieId,  HallId = imax,  SessionDate = new DateOnly(2026,6,16), StartTime = new TimeOnly(15,0),  TicketPrice = 200, TicketsSold = 140 },
            new() { MovieId = movies[9].MovieId,  HallId = imax,  SessionDate = new DateOnly(2026,6,17), StartTime = new TimeOnly(13,0),  TicketPrice = 350, TicketsSold = 190 },
            new() { MovieId = movies[10].MovieId, HallId = hall3d,SessionDate = new DateOnly(2026,6,18), StartTime = new TimeOnly(17,0),  TicketPrice = 220, TicketsSold = 130 },
            new() { MovieId = movies[11].MovieId, HallId = imax,  SessionDate = new DateOnly(2026,6,20), StartTime = new TimeOnly(12,0),  TicketPrice = 300, TicketsSold = 160 }
        };
        db.Sessions.AddRange(sessions);
        db.SaveChanges();
    }
}
