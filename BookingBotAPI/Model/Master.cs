using System.ComponentModel.DataAnnotations;

public class Master
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string TelegramId { get; set; } = string.Empty;

    public List<Service> Services { get; set; } = new();
}
