using ClothingStore.Data;
using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Services;

public class CartService
{
    private readonly AppDbContext _db;

    public CartService(AppDbContext db)
    {
        _db = db;
    }

    // Добавить товар в корзину
    public async Task AddToCartAsync(int productId, int userId)
    {
        var existing = await _db.CartItems
            .FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == userId);

        if (existing != null)
        {
            existing.Quantity++;
        }
        else
        {
            _db.CartItems.Add(new CartItem
            {
                ProductId = productId,
                UserId = userId,
                Quantity = 1
            });
        }

        await _db.SaveChangesAsync();
    }

    // Получить корзину пользователя
    public async Task<List<CartItem>> GetUserCartAsync(int userId)
    {
        return await _db.CartItems
            .Include(c => c.Product)
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }
}