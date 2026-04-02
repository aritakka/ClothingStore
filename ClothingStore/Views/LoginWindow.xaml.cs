using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace ClothingStore.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var main = App.AppHost.Services.GetRequiredService<MainWindow>();
            main.Show();
            this.Close();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            var reg = App.AppHost.Services.GetRequiredService<RegisterWindow>();
            reg.Owner = this;
            reg.ShowDialog();
        }
    }
}
