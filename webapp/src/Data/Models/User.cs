using System.ComponentModel.DataAnnotations;

namespace webapp.src.Data.Models;

public class User {
    [Key]
    public required string ID { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Bithdate { get; set; }
    public required string Phone { get; set; }

    public required ICollection<Order> Orders { get; set; }
}