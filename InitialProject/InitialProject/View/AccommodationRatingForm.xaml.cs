using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationRatingForm.xaml
    /// </summary>
    public partial class AccommodationRatingForm : Window
    {
        private readonly RatedOwnerRepository _ratedOwnerRepository;
        private readonly UnratedOwnerRepository _unratedOwnerRepository;

        public UnratedOwner UnratedOwner { get; set; }
      //  private OwnerMainWindow _ownerMainWindow;
        public AccommodationRatingForm(UnratedOwner unratedOwner)
        {
            InitializeComponent();
            this.DataContext = this;
            UnratedOwner = unratedOwner;
            _ratedOwnerRepository = new RatedOwnerRepository();
            _unratedOwnerRepository = new UnratedOwnerRepository();
           // _ownerMainWindow = ownerMainWindow;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string _additionalComment;
        public string AdditionalComment
        {
            get { return _additionalComment; }
            set
            {
                if (value != _additionalComment)
                {
                    _additionalComment = value;
                    OnPropertyChanged();
                }
            }
        }


        private int _ownerCorrectnessRating;
        public int OwnerCorrectnessRating
        {
            get { return _ownerCorrectnessRating; }
            set
            {
                if (value != _ownerCorrectnessRating)
                {
                    _ownerCorrectnessRating = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _cleanlinessRating;
        public int CleanlinessRating
        {
            get { return _cleanlinessRating; }
            set
            {
                if (value != _cleanlinessRating)
                {
                    _cleanlinessRating = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _imageUrl;
        public int ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                if (value != _imageUrl)
                {
                    _imageUrl = value;
                    OnPropertyChanged();
                }
            }
        }


        private void ButtonClick_Submit(object sender, RoutedEventArgs e)
        {
            RatedOwner ratedOwner = new RatedOwner();
            CreateNewRatedOwner(ratedOwner);

            _ratedOwnerRepository.Save(ratedOwner);
            _unratedOwnerRepository.Remove(UnratedOwner);
          //  _ownerMainWindow.UnratedGuests.Remove(UnratedOwner);
            this.Close();

        }

        private void CreateNewRatedOwner(RatedOwner ratedOwner)
        {
            ratedOwner.Id = UnratedOwner.Id;
            ratedOwner.OwnerCorrectness = _ownerCorrectnessRating;
            ratedOwner.CleanlinessRating = _cleanlinessRating;
            ratedOwner.AdditionalComment = _additionalComment;
           // ratedOwner.ImageUrl = _imageUrl;
            ratedOwner.Reservation = UnratedOwner.Reservation;
        }
       
    }
}
