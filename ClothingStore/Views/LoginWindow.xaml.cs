using System.Windows;
using ClothingStore.Services;

namespace ClothingStore.Views;
public partial class LoginWindow : Window
{
    private readonly AuthService _auth;
    public LoginWindow(AuthService auth)
    {
        InitializeComponent();
        _auth = auth;
    }

    private async void BtnLogin_Click(object sender, RoutedEventArgs e)
    {
        btnLogin.IsEnabled = false;
        var email = txtEmail.Text.Trim();
        var password = txtPassword.Password;

        var user = await _auth.LoginAsync(email, password);
        if (user != null)
        {
            var main = App.AppHost.Services.GetRequiredService<MainWindow>();
            main.Show();
            Close();
        }
        else
        {
            MessageBox.Show("Неверные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            btnLogin.IsEnabled = true;
        }
    }

    private void BtnRegister_Click(object sender, RoutedEventArgs e)
    {
        var reg = App.AppHost.Services.GetRequiredService<RegisterWindow>();
        reg.ShowDialog();
    }
}
