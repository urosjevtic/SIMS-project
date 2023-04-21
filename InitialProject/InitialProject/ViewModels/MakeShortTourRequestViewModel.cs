using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace InitialProject.ViewModels
{
    public class MakeShortTourRequestViewModel : BaseViewModel
    {
        public ICommand UpButtonCommand { get; private set; }
        public ICommand DownButtonCommand { get; private set; }
        private string _nrOfPeople;
        public string NrOfPeople
        {
            get { return _nrOfPeople; }
            set {
                    if (value != _nrOfPeople)
                    {
                        _nrOfPeople = value;
                        OnPropertyChanged(nameof(NrOfPeople));
                    }
                
            }
        }

        public MakeShortTourRequestViewModel()
        {
            UpButtonCommand = new RelayCommand(UpButton);
            DownButtonCommand = new RelayCommand(DownButton);
            NrOfPeople = "0";
        }
        private void UpButton()
        {
            int currentNumber = int.Parse(NrOfPeople);
            NrOfPeople = (currentNumber + 1).ToString();
        }

        private void DownButton()
        {
            int currentNumber = int.Parse(NrOfPeople);
            NrOfPeople = (currentNumber - 1).ToString();
        }
    }
}
