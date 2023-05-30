using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Forms;
using InitialProject.Service;
using InitialProject.View;
using InitialProject.View.Guest2View;
using NuGet.Protocol.Plugins;

namespace InitialProject.ViewModels
{
    public class MyToursViewModel : INotifyPropertyChanged
    {
            private readonly RatedGuideTourService _ratedGuideTourService;
            private readonly TourService _tourService;
            public Tour SelectedActiveTour { get; set; }
            public Tour SelectedEndedTour { get; set; }
            public User LoggedUser { get; set; } = App.LoggedUser;
            public ICommand GoBackCommand { get; private set; }
            public ICommand SubmitRateCommand { get; private set; }
            public ICommand ViewCheckpointsCommand { get; private set; }
            public ICommand RatingCommand { get; private set; }
            public ICommand AddCommand { get; private set; }
            public ICommand OpenCommentFormCommand { get; private set; }

            private ObservableCollection<Tour> _activeTours;
            public ObservableCollection<Tour> ActiveTours
            {
                get { return _activeTours; }
                set
                {
                    if (value != _activeTours)
                    {
                        _activeTours = value;
                        OnPropertyChanged();
                    }
                }
            }
            private ObservableCollection<Tour> _endedTours;
            public ObservableCollection<Tour> EndedTours
            {
                get { return _endedTours; }
                set
                {
                    if (value != _endedTours)
                    {
                        _endedTours = value;
                        OnPropertyChanged();
                    }
                }
            }
            private ObservableCollection<CheckPoint> _checkPoints;
            public ObservableCollection<CheckPoint> CheckPoints
            {
                get { return _checkPoints; }
                set
                {
                    if (value != _checkPoints)
                    {
                        _checkPoints = value;
                        OnPropertyChanged();
                    }
                }
            }

            private int _guideKnowledge = 1;
            public int GuideKnowledge
            {
                get { return _guideKnowledge; }
                set
                {
                    if (value != _guideKnowledge)
                    {
                        _guideKnowledge = value;
                        OnPropertyChanged();
                    }
                }
            }

            private int _guideLanguage = 1;
            public int GuideLanguage
            {
                get { return _guideLanguage; }
                set
                {
                    if (value != _guideLanguage)
                    {
                        _guideLanguage = value;
                        OnPropertyChanged();
                    }
                }
            }
            private int _interestingTour = 1;
            public int InterestingTour
            {
                get { return _interestingTour; }
                set
                {
                    if (value != _interestingTour)
                    {
                        _interestingTour = value;
                        OnPropertyChanged();
                    }
                }
            }
            private string _groupBoxHeader;
            public string GroupBoxHeader
            {
                get { return _groupBoxHeader; }
                set
                {
                    if (value != _groupBoxHeader)
                    {
                        _groupBoxHeader = value;
                        OnPropertyChanged();
                    }
                }
            }
            private string _groupBoxCheckpointsHeader;
            public string GroupBoxCheckpointsHeader
            {
                get { return _groupBoxCheckpointsHeader; }
                set
                {
                    if (value != _groupBoxCheckpointsHeader)
                    {
                        _groupBoxCheckpointsHeader = value;
                        OnPropertyChanged();
                    }
                }
            }
            private ObservableCollection<CheckPoint> _listBox;
            public ObservableCollection<CheckPoint> ListBox
            {
                get { return _listBox; }
                set
                {
                    if (value != _listBox)
                    {
                        _listBox = value;
                        OnPropertyChanged();
                    }
                }
            }
            private string _imageTextBox;
            public string ImageTextBox
            {
                get { return _imageTextBox; }
                set
                {
                    if (value != _imageTextBox)
                    {
                        _imageTextBox = value;
                        OnPropertyChanged();
                    }
                }
            }
            private ObservableCollection<string> _imagelist;
            public ObservableCollection<string> ImageList
            {
                get { return _imagelist; }
                set
                {
                    if (value != _imagelist)
                    {
                        _imagelist = value;
                        OnPropertyChanged();
                    }
                }
            }
        public List<CheckPoint> checkPoints { get; set; }
        public NavigationService navService { get; }
            public MyToursViewModel(NavigationService nav)
            {
                navService = nav;
                _tourService = new TourService();
                _ratedGuideTourService = new RatedGuideTourService();
                ActiveTours = new ObservableCollection<Tour>(_tourService.FindAllMyActiveTours(LoggedUser));
                EndedTours = new ObservableCollection<Tour>(_tourService.FindAllEndedTours());
                GroupBoxHeader = "Rate tour";
                GroupBoxCheckpointsHeader = "Checkpoints";
                GoBackCommand = new RelayCommand(GoBack);
                SubmitRateCommand = new RelayCommand(SubmitRate);
                ViewCheckpointsCommand = new RelayCommand<Tour>(ViewCheckpoints);
                RatingCommand = new RelayCommand<Tour>(Rating);
                OpenCommentFormCommand = new RelayCommand(OpenCommentForm);
                AddCommand = new RelayCommand(AddToList);
                ImageList = new ObservableCollection<string>();
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
            private void GoBack()
            {
                if (navService.CanGoBack)
                {
                    navService.GoBack();
                }
            }
            public void AddToList()
            {
                ImageList.Add(ImageTextBox);
                ImageTextBox = "";
            }
            private void ViewCheckpoints(Tour tour)
            {
                SelectedActiveTour = tour;
                GroupBoxCheckpointsHeader = "Checkpoints: " + SelectedActiveTour.Name;
                checkPoints = SelectedActiveTour.CheckPoints;
                ListBox = new ObservableCollection<CheckPoint>(checkPoints);
            }

            private void SubmitRate()
            {
                if (SelectedEndedTour != null)
                {
                    _ratedGuideTourService.Create(LoggedUser, SelectedEndedTour.Id, GuideKnowledge, GuideLanguage, InterestingTour);
                    _tourService.RateTour(SelectedEndedTour);
                    if (ImageList != null)
                    {
                        _tourService.AddGuestsImage(SelectedEndedTour, ImageList);
                    }
                    MessageBox.Show("Tour sucessfully rated!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    EndedTours.Remove(SelectedEndedTour);
                    EndedTours = new ObservableCollection<Tour>(EndedTours);
                }
                else
                {
                    MessageBox.Show("You didn't select any tour! Click on Button View to select tour!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void OpenCommentForm()
            {
                if (SelectedEndedTour != null)
                {
                    CommentsOverview commentOverview = new(LoggedUser, SelectedEndedTour);
                    commentOverview.Show();
                }
                else
                {
                    MessageBox.Show("You didn't select any tour! Click on Button View to select tour!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void Rating(object tour)
            {
                SelectedEndedTour = (Tour)tour;
                GroupBoxHeader = "Rate tour: " + SelectedEndedTour.Name;
            }
        }
    }

