using ClothingStore.Models;
using ClothingStore.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace ClothingStore.Views
{
    public partial class ProductWindow : Window
    {
        private readonly ProductService _productService;

        public ProductWindow(ProductService productService)
        {
            InitializeComponent();
            _productService = productService;
            this.Loaded += async (_, __) => await LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            try
            {
                var products = await _productService.GetAllAsync();
                lvProducts.ItemsSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось загрузить продукты: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            await LoadProductsAsync();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var add = App.AppHost.Services.GetRequiredService<AddProductWindow>();
            add.Owner = this;
            var res = add.ShowDialog();
            if (res == true)
            {
                _ = LoadProductsAsync();
            }
        }
    }
}
