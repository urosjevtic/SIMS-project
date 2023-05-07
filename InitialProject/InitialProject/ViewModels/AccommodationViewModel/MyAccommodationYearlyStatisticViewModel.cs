using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Statistics;
using InitialProject.Service;
using InitialProject.Service.StatisticService;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.MyAccommodations;

namespace InitialProject.ViewModels.AccommodationViewModel
{
    public class MyAccommodationYearlyStatisticViewModel : BaseViewModel
    {
        public ObservableCollection<AccommodationStatistic> Statistics { get; set; }
        private readonly YearlyAccommodationService _yearlyAccommodationService;
        private readonly AccommodationService _accommodationService;
        private readonly User _logedInUser;
        private readonly int AccommodataionId;
        public  DateTime YearWithMostReservations { get; }
        public Accommodation Accommodation { get; }


        public MyAccommodationYearlyStatisticViewModel(int accommodationId, User logedInUser)
        {
            _yearlyAccommodationService = new YearlyAccommodationService();
            _accommodationService = new AccommodationService();
            _logedInUser = logedInUser;
            AccommodataionId = accommodationId;
            Statistics = new ObservableCollection<AccommodationStatistic>(_yearlyAccommodationService.GetStatisticByAccommodationId(accommodationId));
            YearWithMostReservations = _yearlyAccommodationService.GetYearWithMostReservations(AccommodataionId);
            Accommodation = _accommodationService.GetById(AccommodataionId);
        }

        public ICommand GoBackCommand => new RelayCommand(GoBack);
        private void GoBack()
        {
            MyAccommodationStatisticView myAccommodationsStatistic = new MyAccommodationStatisticView(_logedInUser);
            CloseCurrentWindow();
            myAccommodationsStatistic.Show();
        }


        public ICommand SeeStatisticsByMonthsCommand => new RelayCommandWithParams(SeeStatisticsByMonths);

        private void SeeStatisticsByMonths(object parameter)
        {
            if (parameter is AccommodationStatistic selectedStatistic)
            {
                // Navigate to the other window passing the selected guest as a parameter
                MyAccommodationMonthlyStatisticView myAccommodationMonthlyStatistic =
                    new MyAccommodationMonthlyStatisticView(AccommodataionId, selectedStatistic.MonthAndYear, _logedInUser);
                CloseCurrentWindow();
                myAccommodationMonthlyStatistic.Show();

            }
        }

    }
}
