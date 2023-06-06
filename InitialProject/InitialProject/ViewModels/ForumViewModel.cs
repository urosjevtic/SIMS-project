using GalaSoft.MvvmLight.Messaging;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Forums;
using InitialProject.Service;
using InitialProject.Service.ForumServices;
using InitialProject.Utilities;
using InitialProject.View;
using Microsoft.Expression.Interactivity.Core;
using Notification.Wpf;
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
        private Location location;
        public Forum selectedForum { get; set; }
        public NavigationService NavigationService { get; set; }
        public ObservableCollection<Forum> Forums { get; set; }
        private readonly User _logedInUser=App.LoggedUser;
        private readonly ForumService _forumService;
        private readonly ForumCommentsService _forumCommentsService;
        public ForumComment forumComment;
        public ForumViewModel(NavigationService navigationService)
        {
            forumComment = new ForumComment();
            _forumCommentsService = new ForumCommentsService();
            _accommodationService = new AccommodationService();
            _locationService = new LocationService();
            Locations =_locationService.GetCountriesAndCities();
            location = new Location();
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
       //private string city =City.ToString();
        private bool isOpen = true;
       public  ICommand MakeForumCommand => new RelayCommand(MakeForum);
     
        public void MakeForum()
        {
           InitialProject.Domain.Model.Forums.Forum forum = new Forum();
            List<Forum> Forums = _forumService.GetAll();
            ForumComment forumComment = new ForumComment();
            foreach (var item in Forums)
            {
                if (_city == item.Location.City)
                {
                    if (item.IsOpen == false)
                    {
                        item.IsOpen = true;
                        // pozovi servis da sacuvas forum sada kada je njegov status izmenjen - Sacuvas SelectedForum
                        _forumService.ChangeStatus(item, item.IsOpen);
                        NavigationService.Navigate(new ForumSelectedPage(item, NavigationService));
                        Messenger.Default.Send<ToastNotification>(new ToastNotification("Success", "You have successfully open existing forum.", NotificationType.Success));
                        break;
                    }
                        
                 }
                forumComment = _forumService.AddNewComment(forum, _logedInUser, _comment);
                forum = _forumService.Save(_logedInUser, _country, _city, DateTime.Now, isOpen, forumComment.Comment);
                Messenger.Default.Send<ToastNotification>(new ToastNotification("Success", "You have successfully make the forum.", NotificationType.Success));
                NavigationService.Navigate(new ForumSelectedPage(forum, NavigationService));
                break;
                // forum = _forumService.GetByCity(item.Location.City);
                // NavigationService.Navigate(new ForumSelectedPage(item, NavigationService));
                // break;
            }
        }
       
        }
    }   

 

