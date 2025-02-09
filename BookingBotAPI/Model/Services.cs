using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Service
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int MasterId { get; set; }

    [ForeignKey("MasterId")]
    public Master Master { get; set; } = null!;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int DurationMinutes { get; set; } // Длительность услуги в минутах
}
