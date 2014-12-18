using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SDAS.ViewModels
{
    /// <summary>
    /// 逆布尔值转显隐
    /// </summary>
    public class AntiBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility Result = Visibility.Visible;

            if ((bool)value)
            {
                Result = Visibility.Collapsed;
            }
            else
            {
                Result = Visibility.Visible;
            }

            return Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
