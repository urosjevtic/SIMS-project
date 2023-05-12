using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Service;
using InitialProject.View;
using InitialProject.View.Guest2View;

namespace InitialProject.ViewModels
{
    public class TourSearchViewModel : INotifyPropertyChanged
    {
        private readonly TourService _tourService;
        public User LoggedUser { get; set; }
        public ICommand SearchCommand { get; set; }

        public TourSearchViewModel(User user)
        {
            _tourService = new TourService();
            LoggedUser = user;
            SearchCommand = new RelayCommand(Search);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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
            List<Tour> filteredTours = _tourService.Search(_state, _city, _language, _duration, _numberOfPeople);
            Window currentWindow = System.Windows.Application.Current.Windows.OfType<TourSearch>().SingleOrDefault(w => w.IsActive);
            currentWindow?.Close();
            SearchResult searchResult = new SearchResult(filteredTours, LoggedUser);
            searchResult.Show();
            
        }
    }
}
