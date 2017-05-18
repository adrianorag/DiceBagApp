using System;
using System.Globalization;
using Xamarin.Forms;

//Obs: Namespace to ROOT
namespace DiceBagApp
{
    class LastResultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value.Equals(0))
                return "";

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return 0;

            return value;
        }
    }
}
