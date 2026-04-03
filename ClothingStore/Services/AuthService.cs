using System;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using ClothingStore.Data;
using ClothingStore.Models;

namespace ClothingStore.Services;

public class AuthService
{
    private readonly AppDbContext _db;
    public AuthService(AppDbContext db) => _db = db;

    private static string HashPassword(string password)
    {
        using var rng = RandomNumberGenerator.Create();
        byte[] salt = new byte[16];
        rng.GetBytes(salt);
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
        byte[] key = pbkdf2.GetBytes(32);
        var result = new byte[1 + salt.Length + key.Length];
        result[0] = 0;
        Buffer.BlockCopy(salt, 0, result, 1, salt.Length);
        Buffer.BlockCopy(key, 0, result, 1 + salt.Length, key.Length);
        return Convert.ToBase64String(result);
    }

    private static bool VerifyPassword(string password, string hashed)
    {
        var data = Convert.FromBase64String(hashed);
        if (data[0] != 0) return false;
        byte[] salt = new byte[16];
        Buffer.BlockCopy(data, 1, salt, 0, salt.Length);
        byte[] storedKey = new byte[32];
        Buffer.BlockCopy(data, 1 + salt.Length, storedKey, 0, storedKey.Length);
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
        byte[] key = pbkdf2.GetBytes(32);
        return CryptographicOperations.FixedTimeEquals(key, storedKey);
    }

    public async Task<(bool Success, string? Error)> RegisterAsync(string email, string password, string fullName)
    {
        if (await _db.Users.AnyAsync(u => u.Email == email))
            return (false, "Email уже занят");

        var user = new User
        {
            Email = email,
            PasswordHash = HashPassword(password),
            FullName = fullName
        };

        _db.Users.Add(user);
        try
        {
            await _db.SaveChangesAsync();
            return (true, null);
        }
        catch (DbUpdateException ex)
        {
            // возможен конфликт уникального индекса
            return (false, "Ошибка БД: " + ex.Message);
        }
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u => u.Email == email);
        return user != null && VerifyPassword(password, user.PasswordHash) ? user : null;
    }
}
