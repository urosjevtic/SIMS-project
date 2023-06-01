using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Service;
using InitialProject.View;
using InitialProject.View.Guest2View;

namespace InitialProject.ViewModels
{
    public class TourSearchViewModel : BaseViewModel
    {
        private readonly TourService _tourService;
        public User LoggedUser { get; set; } = App.LoggedUser;
        public ICommand SearchCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand ShowTourCommand { get; set; }
        public ICommand UpButtonCommand { get; private set; }
        public ICommand DownButtonCommand { get; private set; }
        public ICommand upButtonCommand { get; private set; }
        public ICommand downButtonCommand { get; private set; }

        private ObservableCollection<Tour> _filteredTours;
        public ObservableCollection<Tour> filteredTours
        {
            get => _filteredTours;
            set
            {
                if (value != _filteredTours)
                {
                    _filteredTours = value;
                    OnPropertyChanged(nameof(filteredTours));
                }
            }
        }
        public NavigationService navigationService { get; }
        public TourSearchViewModel(NavigationService nav)
        {
            this.navigationService = nav;
            _tourService = new TourService();
            SearchCommand = new RelayCommand(Search);
            GoBackCommand = new RelayCommand(GoBack);
            ShowTourCommand = new RelayCommand<Tour>(ShowSelectedTour);
            UpButtonCommand = new RelayCommand(UpButton);
            DownButtonCommand = new RelayCommand(DownButton);
            upButtonCommand = new RelayCommand(upButton);
            downButtonCommand = new RelayCommand(downButton);
            filteredTours = new ObservableCollection<Tour>();
            NumberOfPeople = "0";
            Duration = "0";
        }
        public TourSearchViewModel(NavigationService nav, ObservableCollection<Tour> Tours)
        {
            this.navigationService = nav;
            _tourService = new TourService();
            SearchCommand = new RelayCommand(Search);
            GoBackCommand = new RelayCommand(GoBack);
            ShowTourCommand = new RelayCommand<Tour>(ShowSelectedTour);
            UpButtonCommand = new RelayCommand(UpButton);
            DownButtonCommand = new RelayCommand(DownButton);
            upButtonCommand = new RelayCommand(upButton);
            downButtonCommand = new RelayCommand(downButton);
            filteredTours = Tours;
            NumberOfPeople = "0";
            Duration = "0";
        }
        private void GoBack()
        {
            navigationService.Navigate(new ShowTourPage(navigationService));
        }
        private void UpButton()
        {
            int currentNumber = int.Parse(NumberOfPeople);
            NumberOfPeople = (currentNumber + 1).ToString();
        }
        private void DownButton()
        {
            int currentNumber = int.Parse(NumberOfPeople);
            NumberOfPeople = (currentNumber - 1).ToString();
        }
        private void upButton()
        {
            int currentNumber = int.Parse(Duration);
            Duration = (currentNumber + 1).ToString();
        }
        private void downButton()
        {
            int currentNumber = int.Parse(Duration);
            Duration = (currentNumber - 1).ToString();
        }
        private string _state;
        public string State
        {
            get => _state;
            set
            {
                if (value != _state)
                {
                    _state = value;
                    OnPropertyChanged(nameof(State));
                }
            }
        }
        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }
        private string _language;
        public string Language
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged(nameof(Language));
                }
            }
        }
        private string _duration;
        public string Duration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }
        private string _numberOfPeople;
        public string NumberOfPeople
        {
            get => _numberOfPeople;
            set
            {
                if (value != _numberOfPeople)
                {
                    _numberOfPeople = value;
                    OnPropertyChanged(nameof(NumberOfPeople));
                }
            }
        }
        public void Search()
        {
            filteredTours = new ObservableCollection<Tour>(_tourService.Search(_state, _city, _language, _duration, _numberOfPeople));
        }
        public void ShowSelectedTour(Tour tour)
        {
            navigationService.Navigate(new SelectedTourPage(tour, navigationService));
        }
    }
}
