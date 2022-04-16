using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using InvoiceMakerCore.ViewModels.Base;

namespace InvoiceMaker.Pages
{
    public class BasePage : UserControl
    {
    }

    public class BasePage<VM> : BasePage
        where VM : BaseViewModel, new()
    {
        private VM _viewModel;

        public VM ViewModel
        {
            get => _viewModel;
            set
            {
                if (_viewModel == value)
                    return;

                _viewModel = value;
                DataContext = _viewModel;
            }
        }

        public BasePage() : base()
        {
            ViewModel = new VM();
        }
    }
}
