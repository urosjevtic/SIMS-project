using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace InitialProject.ViewModels
{
    public class ShowShortTourRequestsViewModel : BaseViewModel
    {
        public ICommand GoBackCommand { get; private set; }
        public ShowShortTourRequestsViewModel()
        {
            GoBackCommand = new RelayCommand(GoBack);
        }

        public void GoBack()
        {
            CloseCurrentWindow();
        }
    }
}
