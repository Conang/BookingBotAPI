using System.ComponentModel.DataAnnotations;

public class Client
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public List<ClientMessenger> Messengers { get; set; } = new();
}