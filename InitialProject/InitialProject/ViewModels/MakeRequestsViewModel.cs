using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using InitialProject.View.Guest2View;

namespace InitialProject.ViewModels
{
    public class MakeRequestsViewModel
    {
        public ICommand MakeShortTourRequestCommand { get; private set; }
        public ICommand MakeComplexTourRequestCommand { get; private set; }

        public MakeRequestsViewModel()
        {
            MakeShortTourRequestCommand = new RelayCommand(MakeShortRequest);
            MakeComplexTourRequestCommand = new RelayCommand(MakeComplexRequest);
        }
        public void MakeShortRequest()
        {
            MakeShortTourRequest makeShort = new();
            Close();
            makeShort.Show();
        }
        public void MakeComplexRequest()
        {
            MakeComplexTourRequest makeComplex = new();
            Close();
            makeComplex.Show();
        }
        public void Close()
        {
            Window currentWindow = Application.Current.Windows.OfType<MakeRequests>().SingleOrDefault(w => w.IsActive);
            currentWindow?.Close();
        }
    }
}
