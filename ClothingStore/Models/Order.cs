namespace ClothingStore.Models;
public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Pending"; // Pending, Shipped, Completed, Cancelled

    public ICollection<OrderItem>? Items { get; set; }
}
