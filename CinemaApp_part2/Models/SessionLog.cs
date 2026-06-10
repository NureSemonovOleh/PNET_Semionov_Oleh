using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Models;

public class SessionLog
{
    public int LogId { get; set; }
    public int SessionId { get; set; }

    [Display(Name = "Дата зміни")]
    public DateTime ModifyDate { get; set; }

    [Display(Name = "Було квитків")]
    public int? OldTicketsSold { get; set; }

    [Display(Name = "Стало квитків")]
    public int? NewTicketsSold { get; set; }

    [StringLength(10)]
    [Display(Name = "Операція")]
    public string Operation { get; set; } = string.Empty;

    public Session? Session { get; set; }
}
