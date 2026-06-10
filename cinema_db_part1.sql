USE master
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'Cinema')
BEGIN
    ALTER DATABASE Cinema SET SINGLE_USER WITH ROLLBACK IMMEDIATE
    DROP DATABASE Cinema
END
GO

CREATE DATABASE Cinema
GO

USE Cinema
GO

PRINT N'>>> База даних Cinema створена'
GO

CREATE TABLE Genres (
    GENRE_ID    int           NOT NULL IDENTITY(1,1),
    NAME        nvarchar(30)  NOT NULL,
    DESCRIPTION nvarchar(200) NULL,
    CONSTRAINT PK_Genres PRIMARY KEY CLUSTERED (GENRE_ID)
)
GO

CREATE TABLE Directors (
    DIRECTOR_ID int          NOT NULL IDENTITY(1,1),
    LAST_NAME   nvarchar(30) NOT NULL,
    FIRST_NAME  nvarchar(30) NOT NULL,
    COUNTRY     nvarchar(30) NULL,
    CONSTRAINT PK_Directors PRIMARY KEY CLUSTERED (DIRECTOR_ID)
)
GO

CREATE TABLE Movies (
    MOVIE_ID    int           NOT NULL IDENTITY(1,1),
    TITLE       nvarchar(100) NOT NULL,
    DIRECTOR_ID int           NOT NULL,
    GENRE_ID    int           NOT NULL,
    YEAR        int           NULL,
    DURATION    int           NULL,
    RATING      float         NULL,
    DESCRIPTION nvarchar(500) NULL,
    CONSTRAINT PK_Movies          PRIMARY KEY CLUSTERED (MOVIE_ID),
    CONSTRAINT FK_Movies_Director FOREIGN KEY (DIRECTOR_ID) REFERENCES Directors(DIRECTOR_ID),
    CONSTRAINT FK_Movies_Genre    FOREIGN KEY (GENRE_ID)    REFERENCES Genres(GENRE_ID)
)
GO

CREATE TABLE Halls (
    HALL_ID   int          NOT NULL IDENTITY(1,1),
    NAME      nvarchar(30) NOT NULL,
    CAPACITY  int          NOT NULL,
    HALL_TYPE nvarchar(20) NULL,
    CONSTRAINT PK_Halls PRIMARY KEY CLUSTERED (HALL_ID)
)
GO

CREATE TABLE Sessions (
    SESSION_ID   int   NOT NULL IDENTITY(1,1),
    MOVIE_ID     int   NOT NULL,
    HALL_ID      int   NOT NULL,
    SESSION_DATE date  NOT NULL,
    START_TIME   time  NOT NULL,
    TICKET_PRICE float NOT NULL,
    TICKETS_SOLD int   NOT NULL DEFAULT 0,
    CONSTRAINT PK_Sessions       PRIMARY KEY CLUSTERED (SESSION_ID),
    CONSTRAINT FK_Sessions_Movie FOREIGN KEY (MOVIE_ID) REFERENCES Movies(MOVIE_ID),
    CONSTRAINT FK_Sessions_Hall  FOREIGN KEY (HALL_ID)  REFERENCES Halls(HALL_ID),
    CONSTRAINT CK_Sessions_Sold  CHECK (TICKETS_SOLD >= 0)
)
GO

CREATE TABLE SessionLogs (
    LOG_ID           int         NOT NULL IDENTITY(1,1),
    SESSION_ID       int         NOT NULL,
    MODIFY_DATE      datetime    NOT NULL,
    OLD_TICKETS_SOLD int         NULL,
    NEW_TICKETS_SOLD int         NULL,
    OPERATION        varchar(10) NOT NULL,
    CONSTRAINT PK_SessionLogs PRIMARY KEY (LOG_ID)
)
GO

PRINT N'>>> 6 таблиць створено'
GO

INSERT INTO Genres (NAME, DESCRIPTION) VALUES
(N'Трилер',     NULL),
(N'Фантастика', NULL),
(N'Кримінал',   NULL),
(N'Пригоди',    NULL),
(N'Жахи',       NULL)
GO

INSERT INTO Directors (LAST_NAME, FIRST_NAME, COUNTRY) VALUES
(N'Нолан',     N'Крістофер', N'Велика Британія'),
(N'Спілберг',  N'Стівен',    N'США'),
(N'Тарантіно', N'Квентін',   N'США'),
(N'Кубрік',    N'Стенлі',    N'США'),
(N'Кемерон',   N'Джеймс',    N'Канада')
GO

INSERT INTO Movies (TITLE, DIRECTOR_ID, GENRE_ID, YEAR, DURATION, RATING, DESCRIPTION) VALUES
(N'Темний Лицар',          1, 1, 2008, 152, 9.0, N'Бетмен проти Джокера'),
(N'Початок',               1, 1, 2010, 148, 8.8, N'Крадіжка ідей уві сні'),
(N'Інтерстеллар',          1, 2, 2014, 169, 8.6, N'Подорож крізь черв''яну нору'),
(N'Парк Юрського Периоду', 2, 4, 1993, 127, 8.1, N'Динозаври оживають'),
(N'Список Шиндлера',       2, 1, 1993, 195, 9.0, N'Порятунок євреїв під час Голокосту'),
(N'Кримінальне чтиво',     3, 3, 1994, 154, 8.9, N'Переплетені кримінальні історії'),
(N'Убити Білла',           3, 3, 2003, 111, 8.1, N'Помста Нареченої'),
(N'Сяяння',                4, 5, 1980, 146, 8.4, N'Письменник у готелі-привиді'),
(N'2001: Космічна одіссея',4, 2, 1968, 149, 8.3, N'Подорож до Юпітера'),
(N'Аватар',                5, 2, 2009, 162, 7.8, N'Людство проти На''аві'),
(N'Титанік',               5, 4, 1997, 194, 7.9, N'Трагедія на кораблі'),
(N'Інопланетянин',         2, 4, 1982, 114, 7.9, N'Хлопчик і прибулець')
GO

INSERT INTO Halls (NAME, CAPACITY, HALL_TYPE) VALUES
(N'Зала IMAX',  200, N'IMAX'),
(N'Зала 3D',    150, N'3D'),
(N'Стандартна', 100, N'Standard')
GO

INSERT INTO Sessions (MOVIE_ID, HALL_ID, SESSION_DATE, START_TIME, TICKET_PRICE, TICKETS_SOLD) VALUES
(1,  1, '2024-02-01', '18:00', 350, 180),
(2,  2, '2024-02-01', '20:30', 250, 120),
(3,  1, '2024-02-02', '18:00', 350, 180),
(4,  3, '2024-02-02', '16:00', 150,  90),
(5,  2, '2024-02-03', '19:00', 250, 100),
(6,  3, '2024-02-03', '21:00', 150,  80),
(7,  2, '2024-02-04', '20:00', 250, 110),
(8,  3, '2024-02-04', '22:00', 150,  70),
(9,  1, '2024-02-05', '20:00', 350, 160),
(10, 1, '2024-02-05', '17:00', 350, 190),
(11, 2, '2024-02-06', '18:00', 250, 140),
(12, 3, '2024-02-06', '16:00', 150,  85),
(1,  2, '2024-02-07', '19:00', 250, 130),
(3,  2, '2024-02-07', '21:00', 250,  95),
(6,  2, '2024-02-08', '20:00', 250,  75)
GO

PRINT N'>>> Тестові дані внесено'
GO

IF OBJECT_ID('dbo.usp_SellTickets', 'P') IS NOT NULL DROP PROCEDURE dbo.usp_SellTickets
GO

CREATE PROCEDURE dbo.usp_SellTickets
    @SessionId INT,
    @Count     INT = 1
AS
BEGIN
    SET NOCOUNT ON

    IF NOT EXISTS (SELECT 1 FROM Sessions WHERE SESSION_ID = @SessionId)
    BEGIN
        PRINT N'Помилка: сеанс #' + CAST(@SessionId AS VARCHAR) + N' не існує'
        RETURN
    END

    DECLARE @Capacity    INT
    DECLARE @CurrentSold INT

    SELECT @Capacity    = h.CAPACITY,
           @CurrentSold = s.TICKETS_SOLD
      FROM Sessions s
      JOIN Halls h ON s.HALL_ID = h.HALL_ID
     WHERE s.SESSION_ID = @SessionId

    IF @CurrentSold + @Count > @Capacity
    BEGIN
        PRINT N'Помилка: недостатньо місць. Доступно: ' +
              CAST(@Capacity - @CurrentSold AS VARCHAR) +
              N', запрошено: ' + CAST(@Count AS VARCHAR)
        RETURN
    END

    UPDATE Sessions
       SET TICKETS_SOLD = TICKETS_SOLD + @Count
     WHERE SESSION_ID = @SessionId

    PRINT N'Продано ' + CAST(@Count AS VARCHAR) + N' квитків на сеанс #' +
          CAST(@SessionId AS VARCHAR)
END
GO

IF OBJECT_ID('dbo.usp_UpdateGenreDescription', 'P') IS NOT NULL DROP PROCEDURE dbo.usp_UpdateGenreDescription
GO

CREATE PROCEDURE dbo.usp_UpdateGenreDescription
    @GenreId INT
AS
BEGIN
    SET NOCOUNT ON

    IF NOT EXISTS (SELECT 1 FROM Genres WHERE GENRE_ID = @GenreId)
    BEGIN
        PRINT N'Помилка: жанр ID=' + CAST(@GenreId AS VARCHAR) + N' не існує'
        RETURN
    END

    DECLARE @DirectorName nvarchar(60)

    SELECT TOP 1 @DirectorName = d.LAST_NAME + N' ' + d.FIRST_NAME
      FROM Movies m
      JOIN Directors d ON m.DIRECTOR_ID = d.DIRECTOR_ID
     WHERE m.GENRE_ID = @GenreId
     GROUP BY d.DIRECTOR_ID, d.LAST_NAME, d.FIRST_NAME
     ORDER BY COUNT(DISTINCT m.MOVIE_ID) DESC

    IF @DirectorName IS NULL
    BEGIN
        PRINT N'У жанрі ID=' + CAST(@GenreId AS VARCHAR) + N' немає фільмів'
        RETURN
    END

    DECLARE @Text nvarchar(200)
    SET @Text = CONVERT(nvarchar(10), GETDATE(), 103) + N' ' + @DirectorName

    UPDATE Genres
       SET DESCRIPTION = @Text
     WHERE GENRE_ID = @GenreId

    PRINT N'Жанр ID=' + CAST(@GenreId AS VARCHAR) + N': "' + @Text + '"'
END
GO

PRINT N'>>> Збережені процедури створено'
GO

IF OBJECT_ID('dbo.fn_CountMoviesAboveAvgRating', 'FN') IS NOT NULL
    DROP FUNCTION dbo.fn_CountMoviesAboveAvgRating
GO

CREATE FUNCTION dbo.fn_CountMoviesAboveAvgRating ()
RETURNS INT
AS
BEGIN
    DECLARE @Count INT
    SET @Count = (
        SELECT COUNT(*) FROM Movies
        WHERE RATING > (SELECT AVG(RATING) FROM Movies WHERE RATING IS NOT NULL)
    )
    RETURN @Count
END
GO

PRINT N'>>> Скалярна функція створена'
GO

IF OBJECT_ID('dbo.fn_RevenuePerGenre', 'IF') IS NOT NULL DROP FUNCTION dbo.fn_RevenuePerGenre
GO

CREATE FUNCTION dbo.fn_RevenuePerGenre ()
RETURNS TABLE
AS
RETURN
(
    SELECT
        g.GENRE_ID,
        g.NAME                               AS GENRE_NAME,
        COUNT(DISTINCT m.MOVIE_ID)           AS MOVIES_COUNT,
        COUNT(s.SESSION_ID)                  AS SESSIONS_COUNT,
        SUM(s.TICKETS_SOLD * s.TICKET_PRICE) AS TOTAL_REVENUE
    FROM Genres g
    LEFT JOIN Movies   m ON g.GENRE_ID = m.GENRE_ID
    LEFT JOIN Sessions s ON m.MOVIE_ID = s.MOVIE_ID
    GROUP BY g.GENRE_ID, g.NAME
)
GO

IF OBJECT_ID('dbo.fn_GetTopSessionsByDirector', 'TF') IS NOT NULL
    DROP FUNCTION dbo.fn_GetTopSessionsByDirector
GO

CREATE FUNCTION dbo.fn_GetTopSessionsByDirector (
    @DirectorLastName nvarchar(30)
)
RETURNS @result TABLE (
    SESSION_ID   INT,
    MOVIE_TITLE  nvarchar(100),
    TICKETS_SOLD INT,
    TICKET_PRICE FLOAT,
    SESSION_DATE DATE,
    MESSAGE      nvarchar(200)
)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Directors WHERE LAST_NAME = @DirectorLastName)
    BEGIN
        INSERT INTO @result VALUES (NULL, NULL, NULL, NULL, NULL, N'Відсутня інформація')
        RETURN
    END

    IF NOT EXISTS (
        SELECT 1 FROM Sessions s
        JOIN Movies    m ON s.MOVIE_ID    = m.MOVIE_ID
        JOIN Directors d ON m.DIRECTOR_ID = d.DIRECTOR_ID
        WHERE d.LAST_NAME = @DirectorLastName
    )
    BEGIN
        INSERT INTO @result VALUES (NULL, NULL, NULL, NULL, NULL, N'Відсутня інформація')
        RETURN
    END

    DECLARE @MaxSold INT
    SELECT @MaxSold = MAX(s.TICKETS_SOLD)
      FROM Sessions s
      JOIN Movies    m ON s.MOVIE_ID    = m.MOVIE_ID
      JOIN Directors d ON m.DIRECTOR_ID = d.DIRECTOR_ID
     WHERE d.LAST_NAME = @DirectorLastName

    INSERT INTO @result (SESSION_ID, MOVIE_TITLE, TICKETS_SOLD, TICKET_PRICE, SESSION_DATE, MESSAGE)
    SELECT s.SESSION_ID, m.TITLE, s.TICKETS_SOLD, s.TICKET_PRICE, s.SESSION_DATE, NULL
      FROM Sessions s
      JOIN Movies    m ON s.MOVIE_ID    = m.MOVIE_ID
      JOIN Directors d ON m.DIRECTOR_ID = d.DIRECTOR_ID
     WHERE d.LAST_NAME = @DirectorLastName
       AND s.TICKETS_SOLD = @MaxSold

    RETURN
END
GO

IF OBJECT_ID('dbo.fn_EveryTenthMovieSessions', 'IF') IS NOT NULL
    DROP FUNCTION dbo.fn_EveryTenthMovieSessions
GO

CREATE FUNCTION dbo.fn_EveryTenthMovieSessions (
    @k FLOAT
)
RETURNS TABLE
AS
RETURN
(
    SELECT
        s.SESSION_ID,
        m.TITLE      AS MOVIE_TITLE,
        d.LAST_NAME  AS DIRECTOR,
        g.NAME       AS GENRE,
        s.SESSION_DATE,
        s.START_TIME,
        s.TICKET_PRICE,
        s.TICKETS_SOLD,
        s.TICKETS_SOLD * s.TICKET_PRICE AS REVENUE
    FROM Sessions s
    JOIN Movies    m ON s.MOVIE_ID    = m.MOVIE_ID
    JOIN Directors d ON m.DIRECTOR_ID = d.DIRECTOR_ID
    JOIN Genres    g ON m.GENRE_ID    = g.GENRE_ID
    JOIN (
        SELECT MOVIE_ID, ROW_NUMBER() OVER (ORDER BY MOVIE_ID) AS RN
          FROM Movies
    ) AS ranked ON m.MOVIE_ID = ranked.MOVIE_ID
    WHERE ranked.RN % 10 = 0
      AND s.TICKETS_SOLD * s.TICKET_PRICE < @k
)
GO

PRINT N'>>> Табличні функції створено'
GO

CREATE TRIGGER tr_SessionLogging
ON dbo.Sessions
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON
    INSERT INTO SessionLogs (SESSION_ID, MODIFY_DATE, OLD_TICKETS_SOLD, NEW_TICKETS_SOLD, OPERATION)
    SELECT d.SESSION_ID, GETDATE(), d.TICKETS_SOLD, i.TICKETS_SOLD, 'UPDATE'
      FROM deleted d
      JOIN inserted i ON d.SESSION_ID = i.SESSION_ID
END
GO

CREATE TRIGGER tr_PreventLastSession_Delete
ON dbo.Sessions
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @SessionId     INT
    DECLARE @MovieId       INT
    DECLARE @OtherSessions INT
    DECLARE @AllowAll      BIT           = 1
    DECLARE @ErrorMsg      nvarchar(400) = N''

    DECLARE del_cur CURSOR LOCAL FAST_FORWARD FOR
        SELECT SESSION_ID, MOVIE_ID FROM deleted

    OPEN del_cur
    FETCH NEXT FROM del_cur INTO @SessionId, @MovieId

    WHILE @@FETCH_STATUS = 0
    BEGIN
        SELECT @OtherSessions = COUNT(*)
          FROM Sessions
         WHERE MOVIE_ID   = @MovieId
           AND SESSION_ID != @SessionId

        IF @OtherSessions = 0
        BEGIN
            SET @AllowAll = 0
            SET @ErrorMsg = @ErrorMsg +
                N'Сеанс ID=' + CAST(@SessionId AS nvarchar(10)) +
                N' є єдиним для фільму ID=' + CAST(@MovieId AS nvarchar(10)) + N'. '
        END

        FETCH NEXT FROM del_cur INTO @SessionId, @MovieId
    END

    CLOSE del_cur
    DEALLOCATE del_cur

    IF @AllowAll = 0
        RAISERROR(N'Операцію скасовано. Неможливо видалити останній сеанс фільму: %s', 16, 1, @ErrorMsg)
    ELSE
        DELETE s FROM Sessions s JOIN deleted d ON s.SESSION_ID = d.SESSION_ID
END
GO

CREATE TRIGGER tr_PreventOverbooking
ON dbo.Sessions
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON

    IF UPDATE(TICKETS_SOLD)
    BEGIN
        DECLARE @SessionId INT
        DECLARE @NewSold   INT
        DECLARE @HallId    INT
        DECLARE @Capacity  INT

        DECLARE ovb_cur CURSOR LOCAL FAST_FORWARD FOR
            SELECT i.SESSION_ID, i.HALL_ID, i.TICKETS_SOLD
              FROM inserted i
             WHERE i.TICKETS_SOLD > 0

        OPEN ovb_cur
        FETCH NEXT FROM ovb_cur INTO @SessionId, @HallId, @NewSold

        WHILE @@FETCH_STATUS = 0
        BEGIN
            SELECT @Capacity = CAPACITY FROM Halls WHERE HALL_ID = @HallId

            IF @NewSold > @Capacity
            BEGIN
                CLOSE ovb_cur
                DEALLOCATE ovb_cur
                ROLLBACK TRANSACTION
                RAISERROR(N'Перепродаж заборонено: сеанс ID=%d, продано=%d, місткість=%d.',
                          16, 1, @SessionId, @NewSold, @Capacity)
                RETURN
            END

            FETCH NEXT FROM ovb_cur INTO @SessionId, @HallId, @NewSold
        END

        CLOSE ovb_cur
        DEALLOCATE ovb_cur
    END
END
GO

PRINT N'>>> Тригери створено'
GO

PRINT N'>>> ДЕМОНСТРАЦІЯ ПРОКРУЧУВАНОГО КУРСОРУ (SCROLL)'
GO

DECLARE @Title    nvarchar(100)
DECLARE @Director nvarchar(60)
DECLARE @Revenue  float

DECLARE film_cur CURSOR SCROLL STATIC READ_ONLY FOR
    SELECT
        m.TITLE,
        d.LAST_NAME + N' ' + d.FIRST_NAME,
        ISNULL(SUM(s.TICKETS_SOLD * s.TICKET_PRICE), 0)
    FROM Movies m
    JOIN Directors d ON m.DIRECTOR_ID = d.DIRECTOR_ID
    LEFT JOIN Sessions s ON m.MOVIE_ID = s.MOVIE_ID
    GROUP BY m.MOVIE_ID, m.TITLE, d.LAST_NAME, d.FIRST_NAME
    ORDER BY 3 DESC

OPEN film_cur

FETCH NEXT FROM film_cur INTO @Title, @Director, @Revenue
WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT N'  ' + @Title + N' | ' + @Director +
          N' | ' + CAST(CAST(@Revenue AS INT) AS nvarchar) + N' грн'
    FETCH NEXT FROM film_cur INTO @Title, @Director, @Revenue
END

FETCH LAST     FROM film_cur INTO @Title, @Director, @Revenue
PRINT N'► LAST:       ' + @Title + N' — ' + CAST(CAST(@Revenue AS INT) AS nvarchar) + N' грн'

FETCH PRIOR    FROM film_cur INTO @Title, @Director, @Revenue
PRINT N'► PRIOR:      ' + @Title + N' — ' + CAST(CAST(@Revenue AS INT) AS nvarchar) + N' грн'

FETCH ABSOLUTE 1 FROM film_cur INTO @Title, @Director, @Revenue
PRINT N'► ABSOLUTE 1: ' + @Title + N' — ' + CAST(CAST(@Revenue AS INT) AS nvarchar) + N' грн'

CLOSE film_cur
DEALLOCATE film_cur
GO

PRINT N'--- ТЕСТ 1: Скалярна функція ---'
SELECT dbo.fn_CountMoviesAboveAvgRating() AS [Фільмів з рейтингом вище середнього]
GO

PRINT N'--- ТЕСТ 2: Таблична функція fn_RevenuePerGenre ---'
SELECT * FROM dbo.fn_RevenuePerGenre() ORDER BY TOTAL_REVENUE DESC
GO

PRINT N'--- ТЕСТ 3: usp_SellTickets (продаж 5 квитків на сеанс 12) ---'
EXEC dbo.usp_SellTickets @SessionId = 12, @Count = 5
SELECT SESSION_ID, TICKETS_SOLD FROM Sessions WHERE SESSION_ID = 12
GO

PRINT N'--- ТЕСТ 4: usp_SellTickets (спроба перепродажу) ---'
EXEC dbo.usp_SellTickets @SessionId = 10, @Count = 20
GO

PRINT N'--- ТЕСТ 5: fn_GetTopSessionsByDirector — Нолан (очікуємо 2 сеанси по 180) ---'
SELECT * FROM dbo.fn_GetTopSessionsByDirector(N'Нолан')
GO

PRINT N'--- ТЕСТ 6: fn_GetTopSessionsByDirector — неіснуючий режисер ---'
SELECT * FROM dbo.fn_GetTopSessionsByDirector(N'Невідомий')
GO

PRINT N'--- ТЕСТ 7: usp_UpdateGenreDescription (жанр 2 — Фантастика) ---'
EXEC dbo.usp_UpdateGenreDescription @GenreId = 2
SELECT GENRE_ID, NAME, DESCRIPTION FROM Genres WHERE GENRE_ID = 2
GO

PRINT N'--- ТЕСТ 8: fn_EveryTenthMovieSessions k=100000 (10-й фільм = Аватар) ---'
SELECT * FROM dbo.fn_EveryTenthMovieSessions(100000)
GO

PRINT N'--- ТЕСТ 9: Тригер аудиту tr_SessionLogging ---'
UPDATE Sessions SET TICKETS_SOLD = 95 WHERE SESSION_ID = 4
SELECT * FROM SessionLogs
GO

PRINT N'--- ТЕСТ 10: Видалення одного з кількох сеансів — ДОЗВОЛЕНО ---'
SELECT s.SESSION_ID, m.TITLE FROM Sessions s JOIN Movies m ON s.MOVIE_ID = m.MOVIE_ID WHERE m.DIRECTOR_ID = 1
DELETE FROM Sessions WHERE SESSION_ID = 14
SELECT s.SESSION_ID, m.TITLE FROM Sessions s JOIN Movies m ON s.MOVIE_ID = m.MOVIE_ID WHERE m.DIRECTOR_ID = 1
GO

PRINT N'--- ТЕСТ 11: Видалення ОСТАННЬОГО сеансу фільму — ЗАБОРОНЕНО ---'
SELECT s.SESSION_ID, m.TITLE FROM Sessions s JOIN Movies m ON s.MOVIE_ID = m.MOVIE_ID WHERE m.MOVIE_ID = 8
DELETE FROM Sessions WHERE SESSION_ID = 8
SELECT s.SESSION_ID, m.TITLE FROM Sessions s JOIN Movies m ON s.MOVIE_ID = m.MOVIE_ID WHERE m.MOVIE_ID = 8
GO

PRINT N'--- ТЕСТ 12: Перепродаж квитків — ЗАБОРОНЕНО ---'
SELECT s.SESSION_ID, h.CAPACITY, s.TICKETS_SOLD FROM Sessions s JOIN Halls h ON s.HALL_ID = h.HALL_ID WHERE s.SESSION_ID = 4
UPDATE Sessions SET TICKETS_SOLD = 105 WHERE SESSION_ID = 4
SELECT TICKETS_SOLD FROM Sessions WHERE SESSION_ID = 4
GO

PRINT N'>>> Всі тести виконано'
GO
