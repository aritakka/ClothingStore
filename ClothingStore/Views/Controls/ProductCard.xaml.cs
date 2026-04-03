using ClothingStore.Models;
using ClothingStore.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ClothingStore.Views.Controls
{
    public partial class ProductCard : UserControl
    {
        public ProductCard()
        {
            InitializeComponent();
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Клик работает"); // тестируем, что кнопка ловится

            try
            {
                var product = this.DataContext as Product;

                if (product == null)
                {
                    MessageBox.Show("Product = null");
                    return;
                }

                var cartService = App.AppHost.Services.GetRequiredService<CartService>();
                var userState = App.AppHost.Services.GetRequiredService<UserState>();

                if (userState.CurrentUser == null)
                {
                    MessageBox.Show("Пользователь не найден");
                    return;
                }

                await cartService.AddToCartAsync(product.Id, userState.CurrentUser.Id);

                MessageBox.Show($"Добавлено: {product.Name}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}