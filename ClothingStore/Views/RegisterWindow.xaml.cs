using System.Windows;

namespace ClothingStore.Views;

public partial class RegisterWindow : Window
{
    public RegisterWindow()
    {
        InitializeComponent();
    }

    private void BtnRegister_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Registered (stub).");
        this.Close();
    }
}
