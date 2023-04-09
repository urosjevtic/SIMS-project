using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InitialProject.Service;
using InitialProject.Utilities;

namespace InitialProject.ViewModels.RatingsViewModel
{
    public class GuestRatingFormViewModel : INotifyPropertyChanged
    {

        private readonly UnratedGuest _unratedGuest;
        private readonly GuestRatingService _guestRatingService;

        public GuestRatingFormViewModel(UnratedGuest unratedGuest)
        {
            _unratedGuest = unratedGuest;
            _guestRatingService = new GuestRatingService();
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
                    OnPropertyChanged(nameof(AdditionalComment));
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
                    OnPropertyChanged(nameof(RuleFollowingRating));
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
                    OnPropertyChanged(nameof(CleanlinessRating));
                }
            }
        }

        public ICommand RateAGuestCommand => new RelayCommand(RateAGuest);

        private void RateAGuest()
        {
            _guestRatingService.SubmitRating(_unratedGuest, _ruleFollowingRating, _cleanlinessRating, _additionalComment);
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            window.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
