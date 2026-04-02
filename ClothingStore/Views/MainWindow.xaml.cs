using System.Collections.ObjectModel;
using System.Windows;
using ClothingStore.Models;

namespace ClothingStore.Views;

public partial class MainWindow : Window
{
    public ObservableCollection<Product> Products { get; } = new();

    public MainWindow()
    {
        InitializeComponent();

        Products.Add(new Product { Id = 1, Name = "T-Shirt", Size = "M", Price = 19.99m });
        Products.Add(new Product { Id = 2, Name = "Jeans", Size = "L", Price = 49.99m });

        DataContext = Products;
    }

    private void BtnRefresh_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Refresh (stub).");
    }
}
