using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Models;

public class Director
{
    public int DirectorId { get; set; }

    [Required(ErrorMessage = "Прізвище обов'язкове")]
    [StringLength(30, ErrorMessage = "Прізвище не може перевищувати 30 символів")]
    [Display(Name = "Прізвище")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ім'я обов'язкове")]
    [StringLength(30, ErrorMessage = "Ім'я не може перевищувати 30 символів")]
    [Display(Name = "Ім'я")]
    public string FirstName { get; set; } = string.Empty;

    [StringLength(30, ErrorMessage = "Країна не може перевищувати 30 символів")]
    [Display(Name = "Країна")]
    public string? Country { get; set; }

    [Display(Name = "Повне ім'я")]
    public string FullName => $"{LastName} {FirstName}";

    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
