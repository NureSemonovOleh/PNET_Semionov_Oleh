using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Models;

public class Hall
{
    public int HallId { get; set; }

    [Required(ErrorMessage = "Назва залу обов'язкова")]
    [StringLength(30, ErrorMessage = "Назва не може перевищувати 30 символів")]
    [Display(Name = "Назва залу")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Місткість обов'язкова")]
    [Range(1, 2000, ErrorMessage = "Місткість від 1 до 2000 місць")]
    [Display(Name = "Місткість")]
    public int Capacity { get; set; }

    [StringLength(20, ErrorMessage = "Тип не може перевищувати 20 символів")]
    [Display(Name = "Тип залу")]
    public string? HallType { get; set; }

    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}
