using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp.src.Data.Models;

public class Order {
    [Key]
    public required string ID { get; set; }
    public required string Ref { get; set; }

    [ForeignKey("user_id")]
    public required User User { get; set; }

    public required string Amont { get; set; }
    public required string DeliveryAdresse { get; set; }
    public required string Status { get; set; }

    public required ICollection<OrderRow> products { get; set; }

}