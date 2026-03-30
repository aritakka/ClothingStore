using ClothingStore.Data;
using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Services;
public class ProductService
{
    private readonly AppDbContext _db;
    public ProductService(AppDbContext db) => _db = db;

    public async Task<List<Product>> GetAllAsync() => await _db.Products.ToListAsync();
    public async Task AddAsync(Product p) { _db.Products.Add(p); await _db.SaveChangesAsync(); }
}
