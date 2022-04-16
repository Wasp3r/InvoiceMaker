using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InvoiceMakerCore.ViewModels.Base;

namespace InvoiceMakerCore.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string Email { get; set; }
        public ICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayParameterizedCommand(Login);
            Email = "TestEmail";
        }

        public void Login(object parameter)
        {
            
        }
    }
}
