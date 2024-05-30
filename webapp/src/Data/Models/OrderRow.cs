using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp.src.Data.Models;

public class OrderRow {
    [Key]
    public required string ID { get; set; }

    [ForeignKey("product_id")]
    public required Product Product { get; set; }

    public required string RefArticle { get; set;}

    public required string Qte { get; set;}

}