using System;
using System.Windows;
using ClothingStore.Services;

namespace ClothingStore.Views
{
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
            try
            {
                var fullName = txtFullName.Text?.Trim() ?? string.Empty;
                var email = txtEmail.Text?.Trim() ?? string.Empty;
                var password = txtPassword.Password ?? string.Empty;

                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Введите email и пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (password.Length < 6)
                {
                    MessageBox.Show("Пароль должен быть не менее 6 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var (success, error) = await _auth.RegisterAsync(email, password, fullName);
                if (!success)
                {
                    MessageBox.Show(error ?? "Ошибка при регистрации.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                MessageBox.Show("Регистрация успешна. Вы можете войти.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при регистрации: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                btnRegister.IsEnabled = true;
            }
        }
    }
}
