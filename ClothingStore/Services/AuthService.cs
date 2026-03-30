using ClothingStore.Data;
using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Services;
public class AuthService
{
    private readonly AppDbContext _db;
    public AuthService(AppDbContext db) => _db = db;

    public async Task<(bool Success, string? Error)> RegisterAsync(string email, string password, string fullName)
    {
        if (await _db.Users.AnyAsync(u => u.Email == email))
            return (false, "Email уже занят");

        var user = new User
        {
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            FullName = fullName
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return (true, null);
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u => u.Email == email);
        return user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash) ? user : null;
    }
}
