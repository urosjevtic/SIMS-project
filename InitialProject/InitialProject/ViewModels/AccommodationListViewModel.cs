using InitialProject.Domain.Model;
using InitialProject.Repository.AccommodationRepo;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Navigation;
using InitialProject.View;

namespace InitialProject.ViewModels
{
    public class AccommodationListViewModel : BaseViewModel
    {
        private NavigationService _navigationService;
        private readonly AccommodationRepository _accommodationRepository;
        private ObservableCollection<Accommodation> _accommodations;

        public ObservableCollection<Accommodation> Accommodations
        {
            get
            {
                return _accommodations;
            }

            set
            {
                if (value != _accommodations)
                {
                    _accommodations = value;
                    OnPropertyChanged("Accommodations");
                }
            }
        }

        public Accommodation SelectedAccommodation { get; set; }
        public Domain.Model.User LoggedUser { get; set; }
        public ICommand ShowAccommodationInfoCommand => new RelayCommand(OnShowAccommodationInfo);
        public ICommand SearchAccommodationCommand => new RelayCommand(OnSearchAccommodation);

        public AccommodationListViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            _accommodationRepository = new AccommodationRepository();
            LoadData();
        }

        private void LoadData()
        {
            Accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
        }

        public void OnSearchAccommodation()
        {
            _navigationService.Navigate(new AccommodationSearchPage());
        }

        private void OnShowAccommodationInfo()
        {
            _navigationService.Navigate(new AccommodationReservationPage(SelectedAccommodation));
        }
    }
}
