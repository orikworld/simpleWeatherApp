using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using simpleWeatherApp.Models.Enum;
using Xamarin.Forms;

namespace simpleWeatherApp.Core.Interactions.Converters
{
    public class DegreesToDirectionConverter : IValueConverter
    {
        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int.TryParse(value.ToString(), out int degrees);

            var direction = (Direction)Math.Round((double)degrees % 360 / 45);

            var description = GetEnumDescription(direction);
            return description;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        private string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        #endregion

    }
}
