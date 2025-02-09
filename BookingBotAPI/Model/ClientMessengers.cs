using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class ClientMessenger
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ClientId { get; set; }

    [ForeignKey("ClientId")]
    public Client Client { get; set; } = null!;

    [Required]
    public MessengerType Messenger { get; set; }

    [Required]
    public string MessengerId { get; set; } = string.Empty; // Telegram ID, WhatsApp номер, VK ID
}
