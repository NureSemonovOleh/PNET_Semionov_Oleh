using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Models;

public class Movie
{
    public int MovieId { get; set; }

    [Required(ErrorMessage = "Назва фільму обов'язкова")]
    [StringLength(100, ErrorMessage = "Назва не може перевищувати 100 символів")]
    [Display(Name = "Назва фільму")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Режисер обов'язковий")]
    [Display(Name = "Режисер")]
    public int DirectorId { get; set; }

    [Required(ErrorMessage = "Жанр обов'язковий")]
    [Display(Name = "Жанр")]
    public int GenreId { get; set; }

    [Range(1888, 2100, ErrorMessage = "Рік повинен бути між 1888 та 2100")]
    [Display(Name = "Рік випуску")]
    public int? Year { get; set; }

    [Range(1, 600, ErrorMessage = "Тривалість від 1 до 600 хвилин")]
    [Display(Name = "Тривалість (хв)")]
    public int? Duration { get; set; }

    [Range(0.0, 10.0, ErrorMessage = "Рейтинг від 0 до 10")]
    [Display(Name = "Рейтинг")]
    public double? Rating { get; set; }

    [StringLength(500, ErrorMessage = "Опис не може перевищувати 500 символів")]
    [Display(Name = "Опис")]
    public string? Description { get; set; }

    public Director? Director { get; set; }
    public Genre? Genre { get; set; }
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}
