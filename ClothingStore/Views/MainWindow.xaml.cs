using System;
using System.Windows;
using ClothingStore.Services;

namespace ClothingStore.Views
{
    public partial class MainWindow : Window
    {
        private readonly ProductService _productService;
        private readonly UserState _userState;

        public MainWindow(ProductService productService, UserState userState)
        {
            InitializeComponent();
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _userState = userState ?? throw new ArgumentNullException(nameof(userState));
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_userState.CurrentUser != null)
                    this.Title = $"Clothing Store — {_userState.CurrentUser.FullName}";

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
            try
            {
                var products = await _productService.GetAllAsync();
                lvProducts.ItemsSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось обновить список: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
