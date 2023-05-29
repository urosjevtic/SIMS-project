using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.Repository.AccommodationRepo;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.Ratings;
using InitialProject.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationShow.xaml
    /// </summary>
    public partial class AccommodationShow : Window //, INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void OnPropertyChanged(string name)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(name));
        //    }
        //}

        //private readonly AccommodationRepository _accommodationRepository;
        //private readonly LocationRepository _locationRepository;
        //private ObservableCollection<Accommodation> _accommodations;

        //public User User { get; set; }
        public User LoggedUser { get; set; }

        //public Accommodation SelectedAccommodation { get; set; }
        //public ObservableCollection<Accommodation> Accommodations
        //{
        //    get
        //    {
        //        return _accommodations;
        //    }

        //    set
        //    {
        //        if (value != _accommodations)
        //        {
        //            _accommodations = value;
        //            OnPropertyChanged("Accommodations");
        //        }
        //    }
        //}

        public AccommodationShow(User user)
        {
            InitializeComponent();
            App.LoggedUser = user;
            this.DataContext = new MainWindowViewModel(NavigationFrame.NavigationService);
            //NavigationFrame.Content=new AccommodationListPage(LoggedUser);
            //_accommodationRepository = new AccommodationRepository();
            //LoggedUser = user;
            //LoadData();

            // Proveri za notifikacije - Ako postoji, prikazi neki novi prozor sa porukom

        }

        //private void LoadData()
        //{
        //    Accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
        //}


        //private void MyReservation_Click(object sender, RoutedEventArgs e)
        //{
        //    MyReservations myReservation = new MyReservations(LoggedUser);
        //    myReservation.Show();
        //}

        //private void DeclinedButton_Click(object sender, RoutedEventArgs e)
        //{
        //    RejectedRequests req = new RejectedRequests();
        //    req.Show();
        //}

        //private void ApprovedButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ApprovedReservations approved=new ApprovedReservations();
        //    approved.Show();
        //}

        //private void PendingAccommodation_Click(object sender, RoutedEventArgs e)
        //{
        //    PendingReservations pending=new PendingReservations();
        //    pending.Show();
        //}
        ////////////////////////////////////////
        ////////////////////////////////////////
        ////////////////////////////////////////
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.LoggedUser = null;
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }

        //private void SearchButton1_Click(object sender, RoutedEventArgs e)
        //{
        //    AccommodationSearch accommodationSearch = new AccommodationSearch(LoggedUser);
        //    accommodationSearch.Show();
        //}

        //private void BackButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    if (Properties.Settings.Default.ShowWizard)
        //    {
        //        WizardWindow wizardWindow = new WizardWindow();
        //        wizardWindow.Owner = this;
        //        wizardWindow.ShowDialog();
        //    }
        //}

        //private void Window_Closing(object sender, CancelEventArgs e)
        //{
        //    Properties.Settings.Default.Save();
        //}
    }
}
