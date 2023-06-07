using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
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
        private readonly YearlyAccommodationStatisticService _yearlyAccommodationStatisticService;
        private readonly AccommodationService _accommodationService;
        private readonly User _logedInUser;
        private readonly int AccommodataionId;
        public  int YearWithMostReservations { get; }
        public Accommodation Accommodation { get; }
        public NavigationService NavigationService { get; set; }


        public MyAccommodationYearlyStatisticViewModel(int accommodationId, User logedInUser, NavigationService navigationService)
        {
            _yearlyAccommodationStatisticService = new YearlyAccommodationStatisticService();
            _accommodationService = new AccommodationService();
            _logedInUser = logedInUser;
            AccommodataionId = accommodationId;
            Statistics = new ObservableCollection<AccommodationStatistic>(_yearlyAccommodationStatisticService.GetStatisticByAccommodationId(accommodationId));
            YearWithMostReservations = _yearlyAccommodationStatisticService.GetMostOccupiedYear(AccommodataionId);
            Accommodation = _accommodationService.GetById(AccommodataionId);
            NavigationService = navigationService;
        }

        public ICommand GoBackCommand => new RelayCommand(GoBack);
        private void GoBack()
        {
            NavigationService.Navigate(new MyAccommodationStatisticView(_logedInUser, NavigationService));
        }


        public ICommand SeeStatisticsByMonthsCommand => new RelayCommandWithParams(SeeStatisticsByMonths);

        private void SeeStatisticsByMonths(object parameter)
        {
            if (parameter is AccommodationStatistic selectedStatistic)
            {
                // Navigate to the other window passing the selected guest as a parameter
                NavigationService.Navigate(new MyAccommodationMonthlyStatisticView(AccommodataionId, selectedStatistic.MonthAndYear, _logedInUser, NavigationService));

            }
        }

    }
}
