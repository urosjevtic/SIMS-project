using InitialProject.Domain.Model;
using InitialProject.Service;
using InitialProject.View.OwnerView.MyAccommodations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.Ratings;

namespace InitialProject.ViewModels.AccommodationViewModel
{
    public class MyAccommodationStatisticsViewModel : BaseViewModel
    {
        public ObservableCollection<Accommodation> Accommodations { get; }
        private readonly AccommodationService _accommodationService;
        private readonly User _logedInUser;

        public MyAccommodationStatisticsViewModel(User logedInUser)
        {
            _accommodationService = new AccommodationService();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAllAccommodationByOwnerId(logedInUser.Id));
            _logedInUser = logedInUser;
        }

        public ICommand GoBackCommand => new RelayCommand(GoBack);
        private void GoBack()
        {
            MyAccommodationsMainWindow myAccommodationsMain = new MyAccommodationsMainWindow(_logedInUser);
            CloseCurrentWindow();
            myAccommodationsMain.Show();
        }

        public ICommand SeeStatisticCommand => new RelayCommandWithParams(SeeStatistic);

        private void SeeStatistic(object parameter)
        {
            if (parameter is Accommodation selectedAccommodation)
            {
                // Navigate to the other window passing the selected guest as a parameter
                MyAccommodationYearlyStatisticView myAccommodationYearlyStatistic =
                    new MyAccommodationYearlyStatisticView(selectedAccommodation.Id);
                CloseCurrentWindow();
                myAccommodationYearlyStatistic.Show();

            }
        }
    }
}
