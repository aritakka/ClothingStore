using System.Windows;
using ClothingStore.Services;

namespace ClothingStore.Views;
public partial class RegisterWindow : Window
{
    private readonly AuthService _auth;
    public RegisterWindow(AuthService auth)
    {
        InitializeComponent();
        _auth = auth;
    }

    private async void BtnRegister_Click(object sender, RoutedEventArgs e)
    {
        btnRegister.IsEnabled = false;
        var fullName = txtFullName.Text?.Trim() ?? "";
        var email = txtEmail.Text.Trim();
        var password = txtPassword.Password;

        var (success, error) = await _auth.RegisterAsync(email, password, fullName);
        if (success)
        {
            MessageBox.Show("Регистрация успешна", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
        else
        {
            MessageBox.Show(error ?? "Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            btnRegister.IsEnabled = true;
        }
    }
}
