using System.ComponentModel.DataAnnotations;

namespace webapp.src.Data.Models;

public class OrderRow {
    [Key]
    public required string ID { get; set; }
    public required Product Product { get; set; }

    public required string RefArticle { get; set;}

    public required string Qte { get; set;}

}