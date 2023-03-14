﻿using InitialProject.Model;
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
        public int CleanlinessRating { get; set; }
        public int RuleFollowingRating { get; set; }

        public UnratedGuest UnratedGuest { get; set; }
        public GuestRatingForm(UnratedGuest unratedGuest)
        {
            InitializeComponent();
            this.DataContext = this;
            UnratedGuest = unratedGuest;
            _ratedGuestRepository = new RatedGuestRepository();
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

        private void CleanlinessSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CleanlinessRating = (int)e.NewValue;
        }

        private void RuleFollowingSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RuleFollowingRating = (int)e.NewValue;
        }

        private void SubmitButton(object sender, RoutedEventArgs e)
        {
            RatedGuest ratedGuest = new RatedGuest();
            ratedGuest.User.Id = UnratedGuest.Id;
            ratedGuest.RuleFollowingRating = RuleFollowingRating;
            ratedGuest.CleanlinessRating = CleanlinessRating;
            ratedGuest.AdditionalComment = AdditionalComment;
            ratedGuest.Accommodation = UnratedGuest.ReservedAccommodation;
            ratedGuest.ReservationStartDate = UnratedGuest.ReservationStartDate;
            ratedGuest.ReservationEndDate = UnratedGuest.ReservationEndDate;


            _ratedGuestRepository.Save(ratedGuest);
            this.Close();   
            
        }
    }
}