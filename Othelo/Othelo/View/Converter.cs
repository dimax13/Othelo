using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;

namespace Othelo.View
{
    public class DiscConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Model.DiscColor)value;
            switch (color)
            {
                case Othelo.Model.DiscColor.NONE:
                    return new SolidColorBrush(Colors.Transparent);
                case Othelo.Model.DiscColor.BLACK:
                    return new SolidColorBrush(Colors.Black);
                case Othelo.Model.DiscColor.WHITE:
                    return new SolidColorBrush(Colors.White);
                case Othelo.Model.DiscColor.PLAYABLE:
                    return new SolidColorBrush(Colors.Orange);
                default:
                    return new SolidColorBrush(Colors.Transparent);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
}
