using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Image = InitialProject.Domain.Model.Image;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationRatingForm.xaml
    /// </summary>
    public partial class AccommodationRatingForm : Window
    {
        private readonly RatedOwnerRepository _ratedOwnerRepository;
        private readonly UnratedOwnerRepository _unratedOwnerRepository;
        private readonly ImageRepository _imageRepository;
        public UnratedOwner UnratedOwner { get; set; }
      //  private OwnerMainWindow _ownerMainWindow;
        public AccommodationRatingForm(UnratedOwner unratedOwner)
        {
            InitializeComponent();
            this.DataContext = this;
            UnratedOwner = unratedOwner;
            _ratedOwnerRepository = new RatedOwnerRepository();
            _unratedOwnerRepository = new UnratedOwnerRepository();
            _imageRepository = new ImageRepository();
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

        private string _imageUrl;
        public string ImageUrl
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
            SaveImages(_imageUrl, 0);
           // ratedOwner.ImageUrl.Id = GetImagesId(_imageUrl);
            ratedOwner.ImageUrl= _imageUrl;
            ratedOwner.Reservation = UnratedOwner.Reservation;
        }
        private void SaveImages(string urls, int entityId)
        {
            Image images = new Image();
            images.EntityLd = entityId;
            string[] imagesUrls = SplitStringByComma(urls);
            foreach (string imageUrl in imagesUrls)
            {
                images.Url.Add(imageUrl);
            }

            _imageRepository.ReturnSaved(images);

        }
        private int GetImagesId(string urls)
        {
            List<Image> allImages = _imageRepository.GetAll();
            string[] imageUrls = SplitStringByComma(urls);
            foreach (Image image in allImages)
            {
                if (image.Url.SequenceEqual(imageUrls))
                {
                    return image.Id;
                }
            }
            throw new Exception("Error has occured");
        }
        private string[] SplitStringByComma(string str)
        {
            return str.Split(new string[] { ", ", "," }, StringSplitOptions.RemoveEmptyEntries);
        }




        //Input validation
        private readonly Regex _positiveNumbersPattern = new Regex("^[1-9][0-9]*$");
        private readonly Regex _imagesUrlPattern = new Regex("\\bhttps?://\\S+\\b(?:,\\s*\\bhttps?://\\S+\\b)*");
        public string Error => null;

       /* public string this[string columnName]
        {
            get
            {
                if (columnName == "")
                {
                    if (string.IsNullOrEmpty(Country))
                        return "Select a country";
                }
                else if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                        return "Select a city";
                }
                else if (columnName == "AccommodationName")
                {
                    if (string.IsNullOrEmpty(AccommodationName))
                        return "Accommodation name is required";
                }
                else if (columnName == "AccommodationTypes")
                {
                    if (string.IsNullOrEmpty(AccommodationTypes))
                        return "Accommodation type is required";
                }
                else if (columnName == "MaxGuests")
                {
                    if (string.IsNullOrEmpty(MaxGuests))
                        return "Maximum number of guests is required";
                    Match match = _positiveNumbersPattern.Match(MaxGuests);
                    if (!match.Success)
                        return "Maximum guests format should be: positive number";
                }
                else if (columnName == "MinReservationDays")
                {
                    if (string.IsNullOrEmpty(MinReservationDays))
                        return "Minimum reservation days is required";
                    Match match = _positiveNumbersPattern.Match(MinReservationDays);
                    if (!match.Success)
                        return "Minimum reservation days format should be: positive number";
                }
                else if (columnName == "CancelationPeriod")
                {
                    if (string.IsNullOrEmpty(CancelationPeriod))
                        return "Cancelation period is required";
                    Match match = _positiveNumbersPattern.Match(CancelationPeriod);
                    if (!match.Success)
                        return "Cancelation period format should be: positive number (number of days for cancelation)";
                }
                else if (columnName == "ImagesUrl")
                {
                    if (string.IsNullOrEmpty(ImageUrl))
                        return "Images are required";
                    Match match = _imagesUrlPattern.Match(ImageUrl);
                    if (!match.Success)
                        return "Images input format should be: url1, url2, ...";
                }
                return null;
            }
        }*/
        private readonly string[] _validatedProperties = { "Location", "AccommodationName", "AccommodationTypes", "MaxGuests", "MinReservationDays", "CancelationPeriod", "ImagesUrl", "Country", "City" };

       /* public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }*/
    }
}
