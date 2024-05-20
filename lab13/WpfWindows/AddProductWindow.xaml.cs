using lab13.Database;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace lab13.WpfWindows
{
    /// <summary>
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {

        ProductsSellsEntities _db;

        string imageProduct = null;

        public AddProductWindow(ProductsSellsEntities db)
        {
            InitializeComponent();

            _db = db;
        }

        private void txbPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            double price = 0;
            try
            {
                price = Convert.ToDouble(txbPrice.Text);
            } catch 
            {
                txtPriceCheck.Text = "Это не число!";
                return;
            }

            if (price < 0) { txtPriceCheck.Text = "Цена должна быть положительной!"; return; }
            try
            {
                if (txbPrice.Text.Split(',').Length > 2 || txbPrice.Text.Split(',')[1].Length > 2) { txtPriceCheck.Text = "Не более двух знаков после запятой!"; return; }
            }
            catch { }

            txtPriceCheck.Text = "";
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Multiselect = false,
                Filter = "JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png"
            };
            if (ofd.ShowDialog() == true)
            {
                var newImagePlayer = File.ReadAllBytes(ofd.FileName);
                imageProduct = Convert.ToBase64String(newImagePlayer);
                var source = new ImageSourceConverter().ConvertFrom(newImagePlayer);
                imgProduct.Source = (ImageSource)source;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(
               string.IsNullOrEmpty(txbPrice.Text)||
               txtPriceCheck.Text != ""||
               string.IsNullOrEmpty(txbName.Text)||
               imageProduct == null
               ) 
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }

            var price = Convert.ToDecimal(txbPrice.Text);

            var newProduct = new Product()
            {
                Name = txbName.Text,
                Price = price,
                Image = imageProduct
            };

            _db.Product.Add(newProduct);

            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return; }
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
