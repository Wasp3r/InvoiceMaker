using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace InvoiceMaker.Converters;

public class BoolToVisibilityConverter : BaseValueConverter<BoolToVisibilityConverter>
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return Visibility.Visible;
        return (bool)value ? Visibility.Hidden : Visibility.Visible;
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return true;
        return (Visibility)value == Visibility.Visible;
    }
}