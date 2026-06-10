using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Models;

public class Session
{
    public int SessionId { get; set; }

    [Required(ErrorMessage = "Фільм обов'язковий")]
    [Display(Name = "Фільм")]
    public int MovieId { get; set; }

    [Required(ErrorMessage = "Зал обов'язковий")]
    [Display(Name = "Зал")]
    public int HallId { get; set; }

    [Required(ErrorMessage = "Дата сеансу обов'язкова")]
    [Display(Name = "Дата сеансу")]
    [DataType(DataType.Date)]
    public DateOnly SessionDate { get; set; }

    [Required(ErrorMessage = "Час початку обов'язковий")]
    [Display(Name = "Час початку")]
    [DataType(DataType.Time)]
    public TimeOnly StartTime { get; set; }

    [Required(ErrorMessage = "Ціна квитка обов'язкова")]
    [Range(0.01, 10000, ErrorMessage = "Ціна від 0.01 до 10000")]
    [Display(Name = "Ціна квитка (грн)")]
    public double TicketPrice { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Кількість проданих квитків не може бути від'ємною")]
    [Display(Name = "Продано квитків")]
    public int TicketsSold { get; set; }

    public Movie? Movie { get; set; }
    public Hall? Hall { get; set; }
    public ICollection<SessionLog> SessionLogs { get; set; } = new List<SessionLog>();

    [Display(Name = "Виручка")]
    public double Revenue => TicketsSold * TicketPrice;
}
