using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace InitialProject.Converters
{
    class ImageListToFirstImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<string> imageURLs && imageURLs.Count > 0)
            {
                return new BitmapImage(new Uri(imageURLs[0]));
            }
            else
            {
                return new BitmapImage(new Uri("../../../Resources/Images/NoImagesPlaceholder.jpg", UriKind.RelativeOrAbsolute));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
