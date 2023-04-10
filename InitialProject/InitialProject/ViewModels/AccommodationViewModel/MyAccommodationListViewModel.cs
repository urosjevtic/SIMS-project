using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Service;
using InitialProject.View.OwnerView.MyAccommodations;
using InitialProject.Utilities;
using System.Windows.Input;

namespace InitialProject.ViewModels
{
    public class MyAccommodationListViewModel : BaseViewModel
    {

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        private readonly AccommodationService _accommodationService;
        private readonly User _logedInUser;
        public MyAccommodationListViewModel(User logedInUser)
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
    }
}
