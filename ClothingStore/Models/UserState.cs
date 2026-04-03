using ClothingStore.Models;

namespace ClothingStore.Services;

public class UserState
{
    public User? CurrentUser { get; private set; }
    public void SetUser(User user) => CurrentUser = user;
    public void Clear() => CurrentUser = null;
}
