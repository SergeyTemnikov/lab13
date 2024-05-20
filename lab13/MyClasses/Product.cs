using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace lab13.Database
{
    public partial class Product
    {
        public ImageSource ImageProduct
        {
            get
            {
                var imageByte = Convert.FromBase64String(Image);
                ImageSource source = (ImageSource)new ImageSourceConverter().ConvertFrom(imageByte);
                return source;
            }
        }
    }
}
