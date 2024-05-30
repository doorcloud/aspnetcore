using System.ComponentModel.DataAnnotations;

namespace MyEcommerceApi.Models;

public class Product {
    [Key]
    public required string ID { get; set; }
    public required string Label { get; set; }
    public required string Type { get; set; }
    public required string Qte { get; set; }
    public required string Price { get; set; }

    [Timestamp] // This marks the property for concurrency checking
    public byte[] RowVersion { get; set; }
}