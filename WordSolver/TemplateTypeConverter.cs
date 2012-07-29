using System;
using System.Linq;
using System.Windows.Data;
using System.Windows.Controls;
using System.Collections.Generic;

namespace WordSolver
{
    public class TemplateTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {   
            if (value == null)
                return null;

            if (parameter == null)
                throw new ArgumentNullException("parameter");

            var items = parameter as IEnumerable<object>;
            TemplateType passedValue = (TemplateType)value;
            var listItems = items.OfType<ListBoxItem>().Where(l => (TemplateType)Enum.Parse(typeof(TemplateType), l.Tag as string, true) == passedValue);
            return listItems.First();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return TemplateType.None;
            var passedItem = value as ListBoxItem;
            if (passedItem == null)
                return TemplateType.None;
            else
                return Enum.Parse(typeof(TemplateType), passedItem.Tag as string, true);
        }
    }
}
