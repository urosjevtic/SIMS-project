using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Service;
using InitialProject.View.Guest2View;

namespace InitialProject.ViewModels
{
    public class MakeShortTourRequestViewModel : BaseViewModel
    {
        public ICommand UpButtonCommand { get; private set; }
        public ICommand DownButtonCommand { get; private set; }
        public ICommand SubmitCommand { get; private set; }
        public ICommand GoBackCommand { get; private set; }
        public ShortTourRequestService _shortTourService { get; set; }
        public User LoggedUser { get; set; } = App.LoggedUser;

        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged(nameof(Country));
                }

            }
        }
        private string _city;
        public string City
        {
            get { return _city; }
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
            get { return _language; }
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged(nameof(Language));
                }

            }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }

            }
        }
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
        private string _from;
        public string From
        {
            get { return _from; }
            set
            {
                if (value != _from)
                {
                    _from = value;
                    OnPropertyChanged(nameof(From));
                }

            }
        }
        private string _to;
        public string To
        {
            get { return _to; }
            set
            {
                if (value != _to)
                {
                    _to = value;
                    OnPropertyChanged(nameof(To));
                }

            }
        }
        public NavigationService navService { get; }
        public MakeShortTourRequestViewModel(NavigationService nav)
        {
            this.navService = nav;
            UpButtonCommand = new RelayCommand(UpButton);
            DownButtonCommand = new RelayCommand(DownButton);
            SubmitCommand = new RelayCommand(Submit);
            GoBackCommand = new RelayCommand(GoBack);
            _shortTourService = new ShortTourRequestService();
            NrOfPeople = "0";
        }
        private void GoBack()
        {
            navService.Navigate(new ShowTourPage(navService));
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
        private void Submit()
        {
            if(int.Parse(NrOfPeople) <= 0)
            {
                MessageBox.Show("Number of people must be greater than zero!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(Country == null || Country == "" || City == "" || City == null || Language == "" || Language == null || From == "" || From == null || To == "" || To == null)
            {
                MessageBox.Show("You did not enter all parameters!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (Convert.ToDateTime(From, CultureInfo.InvariantCulture) >= Convert.ToDateTime(To, CultureInfo.InvariantCulture))
            {
                MessageBox.Show("From date is bigger than To date!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _shortTourService.SaveShortRequest(LoggedUser, Country, City, Language, NrOfPeople, Description, Convert.ToDateTime(From, CultureInfo.InvariantCulture), Convert.ToDateTime(To, CultureInfo.InvariantCulture));
                MessageBox.Show("Request successfully created!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                Country = "";
                City = "";
                Language = "";
                Description = "";
                NrOfPeople = "0";
                From = "";
                To = "";
            }
        }
    }
}
