using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Forums;
using InitialProject.Service;
using InitialProject.Service.ForumServices;
using InitialProject.Utilities;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace InitialProject.ViewModels
{
    public class ForumViewModel :BaseViewModel
    {
        private readonly AccommodationService _accommodationService;
        private readonly LocationService _locationService;
        public Forum selectedForum { get; set; }
        public NavigationService NavigationService { get; set; }
        public ObservableCollection<Forum> Forums { get; set; }
        private readonly User _logedInUser=App.LoggedUser;
        private readonly ForumService _forumService;
        public ForumViewModel(NavigationService navigationService)
        {
            _accommodationService = new AccommodationService();
            _locationService = new LocationService();
            Locations = _locationService.GetCountriesAndCities();
          
            NavigationService = navigationService;
            _forumService = new ForumService();
            var forumList = _forumService.GetAll();
            Forums = new ObservableCollection<Forum>();

            foreach (var forum in forumList)
            {
                // Set the IsSpecial value for each forum
                forum.IsSpecial = _forumService.IsSpecial(forum);

                Forums.Add(forum);
            }
        }

        public Dictionary<string, List<string>> Locations { get; set; }



        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                Cities = Locations[_country];
                OnPropertyChanged(nameof(Country));
            }
        }

        private IEnumerable<string> _cities;
        public IEnumerable<string> Cities
        {
            get { return _cities; }
            set
            {
                _cities = Locations[Country];
                OnPropertyChanged(nameof(Cities));
            }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;   
                OnPropertyChanged(nameof(Comment)); 
            }
        }


        private bool _isSpecial;

        public bool IsSpecial
        {
            get { return _isSpecial; }
            set
            {
                _isSpecial = value;
                OnPropertyChanged("IsSpecial");
            }
        }
        public ICommand SelectedForumCommand => new RelayCommandWithParams(OpenSelectedForum);

        private void OpenSelectedForum(object parameter)
        {
              if (parameter is Forum selectedForum)
             {
            // NavigationService.Navigate(new SelecetedForumPage(selectedForum, NavigationService));
            NavigationService.Navigate(new ForumSelectedPage(selectedForum, NavigationService));
            }
        }
    }

 
}
