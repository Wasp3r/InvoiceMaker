using System.Windows;
using System.Windows.Controls;

namespace InvoiceMaker.AttachedProperties;

public class MonitorPasswordProperty : BaseAttachedProperty<MonitorPasswordProperty, bool>
{
    public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        var passwordBox = sender as PasswordBox;
        if (passwordBox == null) return;

        passwordBox.PasswordChanged -= PasswordChanged;
        if (!(bool)e.NewValue) return;
        PasswordBoxHasTextProperty.SetValue((PasswordBox)sender);
        passwordBox.PasswordChanged += PasswordChanged;
    }

    private void PasswordChanged(object sender, RoutedEventArgs e)
    {
        PasswordBoxHasTextProperty.SetValue((PasswordBox)sender);
    }
}