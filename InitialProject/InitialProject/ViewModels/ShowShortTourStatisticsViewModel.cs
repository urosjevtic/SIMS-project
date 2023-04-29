using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;

namespace InitialProject.ViewModels
{
    public class ShowShortTourStatisticsViewModel : BaseViewModel
    {
        public ICommand GoBackCommand { get; set; }
        public List<DataPoint> ToursByLanguage { get; set; }
        public List<DataPoint> ToursByLocation { get; set; }

        private string _comboBoxSelection;
        public string ComboBoxSelection
        {
            get { return _comboBoxSelection; }
            set
            {
                if (_comboBoxSelection != value)
                {
                    _comboBoxSelection = value;
                    OnPropertyChanged(nameof(ComboBoxSelection));
                    Refresh(_comboBoxSelection);
                }
            }
        }
        public ShowShortTourStatisticsViewModel()
        {
            GoBackCommand = new RelayCommand(CloseCurrentWindow);
            ToursByLanguage = new List<DataPoint>()
            {
                new DataPoint { Key = "Jan", Value = 10 },
                new DataPoint { Key = "Feb", Value = 20 },
                new DataPoint { Key = "Mar", Value = 15 },
                new DataPoint { Key = "Apr", Value = 25 },
                new DataPoint { Key = "May", Value = 30 },
                new DataPoint { Key = "Jun", Value = 20 },
            };
            ToursByLocation = new List<DataPoint>();
        }
        public void Refresh(string Year)
        {
           
        }

    }
}
