using InitialProject.Model;
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
    /// Interaction logic for GuestRatingForm.xaml
    /// </summary>
    public partial class GuestRatingForm : Window
    {
        private readonly RatedGuestRepository _ratedGuestRepository;
        private readonly UnratedGuestRepository _unratedGuestRepository;

        public UnratedGuest UnratedGuest { get; set; }
        private OwnerMainWindow _ownerMainWindow;
        public GuestRatingForm(UnratedGuest unratedGuest, OwnerMainWindow ownerMainWindow)
        {
            InitializeComponent();
            this.DataContext = this;
            UnratedGuest = unratedGuest;
            _ratedGuestRepository = new RatedGuestRepository();
            _unratedGuestRepository = new UnratedGuestRepository();
            _ownerMainWindow = ownerMainWindow;
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


        private int _ruleFollowingRating;
        public int RuleFollowingRating
        {
            get { return _ruleFollowingRating; }
            set
            {
                if (value != _ruleFollowingRating)
                {
                    _ruleFollowingRating = value;
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


        private void SubmitButton(object sender, RoutedEventArgs e)
        {
            RatedGuest ratedGuest = new RatedGuest();
            ratedGuest.User.Id = UnratedGuest.Id;
            ratedGuest.RuleFollowingRating = _ruleFollowingRating;
            ratedGuest.CleanlinessRating = _cleanlinessRating;
            ratedGuest.AdditionalComment = _additionalComment;
            ratedGuest.Accommodation = UnratedGuest.ReservedAccommodation;
            ratedGuest.ReservationStartDate = UnratedGuest.ReservationStartDate;
            ratedGuest.ReservationEndDate = UnratedGuest.ReservationEndDate;

            _ratedGuestRepository.Save(ratedGuest);
            _unratedGuestRepository.Remove(UnratedGuest);
            _ownerMainWindow.UnratedGuests.Remove(UnratedGuest);
            this.Close();   
            
        }
    }
}
