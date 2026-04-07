namespace ClothingStore.Models;
public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Size { get; set; }
    public decimal Price { get; set; }
    public string? ImagePath { get; set; }

    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    public int? BrandId { get; set; }
    public Brand? Brand { get; set; }

    public ICollection<Review>? Reviews { get; set; }
}