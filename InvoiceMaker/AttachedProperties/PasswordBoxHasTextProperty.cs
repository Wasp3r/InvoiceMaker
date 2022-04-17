using System.Windows;
using System.Windows.Controls;

namespace InvoiceMaker.AttachedProperties;

public class PasswordBoxHasTextProperty : BaseAttachedProperty<PasswordBoxHasTextProperty, bool>
{
    public static void SetValue(DependencyObject sender)
    {
        SetValue(sender, ((PasswordBox)sender).SecurePassword.Length > 0);
    }
}