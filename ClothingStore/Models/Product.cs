namespace ClothingStore.Models;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Size { get; set; } = "";
    public string? ImagePath { get; set; }
}
