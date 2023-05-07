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
using InitialProject.View.OwnerView.Ratings;

namespace InitialProject.ViewModels.RatingsViewModel
{
    public class GuestRatingFormViewModel : BaseViewModel
    {

        public UnratedGuest UnratedGuest { get; }
        private readonly GuestRatingService _guestRatingService;
        private readonly User _logedInUser;
        public Navigator Navigator { get; set; }

        public GuestRatingFormViewModel(User logedInUser, UnratedGuest unratedGuest, Navigator navigator)
        {
            UnratedGuest = unratedGuest;
            _guestRatingService = new GuestRatingService();
            _logedInUser = logedInUser;
            Navigator = navigator;
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
            _guestRatingService.SubmitRating(UnratedGuest, _ruleFollowingRating, _cleanlinessRating, _additionalComment);
            Navigator.NavigateTo(new UnratedGuestsListView(_logedInUser, Navigator));
        }

        public ICommand GoBackCommand => new RelayCommand(GoBack);

        private void GoBack()
        {
            Navigator.NavigateTo(new UnratedGuestsListView(_logedInUser, Navigator));
        }

    }
}
