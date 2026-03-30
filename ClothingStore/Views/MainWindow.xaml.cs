using System.Windows;
using ClothingStore.Services;
using ClothingStore.Models;

namespace ClothingStore.Views;
public partial class MainWindow : Window
{
    private readonly ProductService _productService;
    public MainWindow(ProductService productService)
    {
        InitializeComponent();
        _productService = productService;
        Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(object? sender, RoutedEventArgs e)
    {
        lvProducts.ItemsSource = await _productService.GetAllAsync();
    }

    private async void BtnRefresh_Click(object sender, RoutedEventArgs e)
    {
        lvProducts.ItemsSource = await _productService.GetAllAsync();
    }
}
