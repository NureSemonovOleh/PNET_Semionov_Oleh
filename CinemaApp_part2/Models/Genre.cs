using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Models;

public class Genre
{
    public int GenreId { get; set; }

    [Required(ErrorMessage = "Назва жанру обов'язкова")]
    [StringLength(30, ErrorMessage = "Назва не може перевищувати 30 символів")]
    [Display(Name = "Назва жанру")]
    public string Name { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "Опис не може перевищувати 200 символів")]
    [Display(Name = "Опис")]
    public string? Description { get; set; }

    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
