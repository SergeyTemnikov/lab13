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
using System.Windows.Shapes;
using lab13.Database;

namespace lab13.WpfWindows
{
    /// <summary>
    /// Логика взаимодействия для ProductSalesWindow.xaml
    /// </summary>
    public partial class ProductSalesWindow : Window
    {

        Product product = null;
        ProductSalesEntities _db;

        public ProductSalesWindow(Product product, ProductSalesEntities db)
        {
            InitializeComponent();

            _db = db;
            this.product = product;

            dgSales.ItemsSource = _db.ProductSales.Where(x => x.Id_Product == product.Id_Product).OrderByDescending(x => x.Sale_Date).ToList();
            txtProduct.Text = "Продажи товара:" + product.Name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
