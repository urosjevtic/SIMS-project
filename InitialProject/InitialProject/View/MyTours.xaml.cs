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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Command;
using InitialProject.Forms;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.Service;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for MyTours.xaml
    /// </summary>
    public partial class MyTours : Window
    {
        private readonly RatedGuideTourService _ratedGuideTourService;
        private readonly TourService _tourService;
        private readonly NotificationService _notificationService;
        public Tour SelectedActiveTour { get; set; }
        public Tour SelectedEndedTour { get; set; }
        public List<Tour> ActiveTours { get; set; }
        public List<Tour> EndedTours { get; set; }
        public List<CheckPoint> Checkpoints { get; set; }
        public User LoggedUser { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private int _guideKnowledge;
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

        private int _guideLanguage;
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
        private int _interestingTour;
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
        public MyTours(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            LoggedUser = user;
            _tourService = new TourService();
            _ratedGuideTourService = new RatedGuideTourService();
            _notificationService = new NotificationService();
            ActiveTours = _tourService.FindAllActiveTours();
            EndedTours = _tourService.FindAllEndedTours();
            activeTours.ItemsSource = new ObservableCollection<Tour>(ActiveTours);
            endedTours.ItemsSource = new ObservableCollection<Tour>(EndedTours);
            groupBoxRate.Header = "Rate tour:";
        }
        private void GoBackButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ViewCheckpointsButton(object sender, RoutedEventArgs e)
        {
            SelectedActiveTour = (Tour)((Button)sender).DataContext;
            // Set the SelectedTour property to the selected tour item
            Checkpoints = SelectedActiveTour.CheckPoints;
            listBox.ItemsSource = new ObservableCollection<CheckPoint>(Checkpoints);
        }

        private void SubmitRateButton(object sender, RoutedEventArgs e)
        {
            if (SelectedEndedTour != null)
            {
                _ratedGuideTourService.Create(LoggedUser, SelectedEndedTour.Id, GuideKnowledge, GuideLanguage, InterestingTour);
                _tourService.RateTour(SelectedEndedTour);
                if(imageTextBox.Text != "")
                {
                    _tourService.AddGuestsImage(SelectedEndedTour, imageTextBox.Text);
                }
                MessageBox.Show("Tour sucessfully rated!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                EndedTours.Remove(SelectedEndedTour);
                endedTours.ItemsSource = new ObservableCollection<Tour>(EndedTours);
            } else
            {
                MessageBox.Show("You didn't select any tour! Click on Button View to select tour!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenCommentFormButton(object sender, RoutedEventArgs e)
        {
            if (SelectedEndedTour != null)
            {
                CommentsOverview commentOverview = new(LoggedUser, SelectedEndedTour);
                commentOverview.Show();
            } else
            {
                MessageBox.Show("You didn't select any tour! Click on Button View to select tour!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RatingButton(object sender, RoutedEventArgs e)
        {
            SelectedEndedTour = (Tour)((Button)sender).DataContext;
            groupBoxRate.Header = "Rate tour: " + SelectedEndedTour.Name;
        }

        private void JoinTourButton(object sender, RoutedEventArgs e)
        {
            if (SelectedActiveTour == null)
            {
                MessageBox.Show("You didn't select any tour! Click on Button View to select tour!", "Mistake", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (!_notificationService.Create(SelectedActiveTour, LoggedUser))
                {
                    MessageBox.Show("You have already sent request for this tour.", "Information", MessageBoxButton.OK);
                }
                else
                {
                    _notificationService.Create(SelectedActiveTour, LoggedUser);
                    MessageBox.Show("Join request is successfully sent", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        } 
    }
}
