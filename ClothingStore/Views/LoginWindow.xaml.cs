using ClothingStore.Models;
using ClothingStore.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace ClothingStore.Views
{
    public partial class LoginWindow : Window
    {
        private readonly AuthService _auth;
        private readonly UserState _userState;

        public LoginWindow(AuthService auth, UserState userState)
        {
            InitializeComponent();
            _auth = auth;
            _userState = userState;
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            btnLogin.IsEnabled = false;
            try
            {
                var email = txtEmail.Text?.Trim() ?? string.Empty;
                var password = txtPassword.Password ?? string.Empty;

                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Введите email и пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var user = await _auth.LoginAsync(email, password);
                if (user is null)
                {
                    MessageBox.Show("Неверный email или пароль.", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _userState.SetUser(user);

                var main = App.AppHost.Services.GetRequiredService<MainWindow>();
                main.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при входе: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                btnLogin.IsEnabled = true;
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            var reg = App.AppHost.Services.GetRequiredService<RegisterWindow>();
            reg.Owner = this;
            reg.ShowDialog();
        }
    }
}
