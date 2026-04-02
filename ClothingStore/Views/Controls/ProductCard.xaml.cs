using System.Windows;
using System.Windows.Controls;
using ClothingStore.Models;

namespace ClothingStore.Views.Controls;

public partial class ProductCard : UserControl
{
    public ProductCard()
    {
        InitializeComponent();
    }

    private void BtnAdd_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is Product p)
            MessageBox.Show($"Added to cart: {p.Name} (stub).");
        else
            MessageBox.Show("Added to cart (stub).");
    }
}
