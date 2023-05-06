using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.View.Guest2View;

namespace InitialProject.ViewModels
{
    public class MakeRequestsViewModel : BaseViewModel
    {
        public ICommand MakeShortTourRequestCommand { get; private set; }
        public ICommand MakeComplexTourRequestCommand { get; private set; }
        public User LoggedUser { get; set; }

        public MakeRequestsViewModel(User user)
        {
            MakeShortTourRequestCommand = new RelayCommand(MakeShortRequest);
            MakeComplexTourRequestCommand = new RelayCommand(MakeComplexRequest);
            LoggedUser = user;
        }
        public void MakeShortRequest()
        {
            MakeShortTourRequest makeShort = new(LoggedUser);
            CloseCurrentWindow();
            makeShort.Show();
        }
        public void MakeComplexRequest()
        {
            MakeComplexTourRequest makeComplex = new();
            CloseCurrentWindow();
            makeComplex.Show();
        }
    }
}
