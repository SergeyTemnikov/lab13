using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using lab13.Database;

namespace lab13.WpfWindows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ProductSalesEntities _db = new ProductSalesEntities();


        public MainWindow()
        {
            InitializeComponent();

            listProduct.ItemsSource = _db.Product.ToList();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow window = new AddProductWindow(_db);
            window.ShowDialog();
            listProduct.ItemsSource = null;
            listProduct.ItemsSource = _db.Product.ToList();
        }

        private void OpenProduct_Click(object sender, RoutedEventArgs e)
        {
            if(listProduct.SelectedValue != null)
            {
                ProductSalesWindow window = new ProductSalesWindow(listProduct.SelectedValue as Product, _db);
                window.ShowDialog();              
            }
            else
            {
                MessageBox.Show("Вы не выбрали товар!");
            }
            
        }

        private void listProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listProduct.SelectedValue != null)
            {
                ProductSalesWindow window = new ProductSalesWindow(listProduct.SelectedValue as Product, _db);
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Вы не выбрали товар!");
            }
        }
    }
}
