using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.Repository.AccommodationRepo;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationListPage.xaml
    /// </summary>
    public partial class AccommodationListPage : Page
    {

    //    public event PropertyChangedEventHandler PropertyChanged;
    //    protected virtual void OnPropertyChanged(string name)
    //    {
    //        if (PropertyChanged != null)
    //        {
    //            PropertyChanged(this, new PropertyChangedEventArgs(name));
    //        }
    //    }

    //    private readonly AccommodationRepository _accommodationRepository;
    //    private readonly LocationRepository _locationRepository;
    //    private ObservableCollection<Accommodation> _accommodations;

    //    public User User { get; set; }
    //    public User LoggedUser { get; set; }

    //    public Accommodation SelectedAccommodation { get; set; }
    //    public ObservableCollection<Accommodation> Accommodations
    //    {
    //        get
    //        {
    //            return _accommodations;
    //        }

    //        set
    //        {
    //            if (value != _accommodations)
    //            {
    //                _accommodations = value;
    //                OnPropertyChanged("Accommodations");
    //            }
    //        }
    //    }

    //    public AccommodationListPage(User user)
    //    {
    //        InitializeComponent();
    //        this.DataContext = this;

    //        _accommodationRepository = new AccommodationRepository();
    //        LoggedUser = user;
    //        LoadData();

    //        // Proveri za notifikacije - Ako postoji, prikazi neki novi prozor sa porukom
    //    }


    //    private void LoadData()
    //    {
    //        Accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
    //    }
    //
    }
}
