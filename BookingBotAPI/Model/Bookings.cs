using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Booking
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ClientId { get; set; }

    [ForeignKey("ClientId")]
    public Client Client { get; set; } = null!;

    [Required]
    public int ServiceId { get; set; }

    [ForeignKey("ServiceId")]
    public Service Service { get; set; } = null!;

    [Required]
    public DateTime AppointmentTime { get; set; } // Дата и время записи

    public BookingStatus Status { get; set; } = BookingStatus.Confirmed;
}
