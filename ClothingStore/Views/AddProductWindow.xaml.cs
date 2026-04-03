using System;
using System.Globalization;
using System.Windows;
using ClothingStore.Models;
using ClothingStore.Services;

namespace ClothingStore.Views
{
    public partial class AddProductWindow : Window
    {
        private readonly ProductService _productService;

        public AddProductWindow(ProductService productService)
        {
            InitializeComponent();
            _productService = productService;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            btnSave.IsEnabled = false;
            try
            {
                var name = txtName.Text?.Trim() ?? string.Empty;
                var size = txtSize.Text?.Trim();
                var img = txtImage.Text?.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Имя продукта обязательно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!decimal.TryParse(txtPrice.Text?.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out var price))
                {
                    MessageBox.Show("Неверная цена. Используйте десятичный формат (например 49.99).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var p = new Product
                {
                    Name = name,
                    Size = string.IsNullOrWhiteSpace(size) ? null : size,
                    Price = price,
                    ImagePath = string.IsNullOrWhiteSpace(img) ? null : img
                };

                await _productService.AddAsync(p);
                MessageBox.Show("Продукт добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                btnSave.IsEnabled = true;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
