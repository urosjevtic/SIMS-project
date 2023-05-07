using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InitialProject.Domain.Model;
using InitialProject.Service;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.MyAccommodations;

namespace InitialProject.ViewModels.AccommodationViewModel
{
    public class MyAccommodationImagesViewModel :BaseViewModel
    {
        private readonly ImageService _imageService;
        private readonly Image _images;
        private readonly User _logedInUser;
        private int ImageCounter = 0;
        public Navigator Navigator { get; set; }

        public Accommodation Accommodation { get; }
        public MyAccommodationImagesViewModel(User logedInUser, Accommodation accommodation, Navigator navigator)
        {
            _imageService = new ImageService();
            _logedInUser = logedInUser;
            Accommodation = accommodation;
            _images = _imageService.GetById(Accommodation.Images.Id);
            _imageUrl = _images.Url[ImageCounter];
            Navigator = navigator;
        }


        private string _imageUrl;

        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value; 
                OnPropertyChanged("ImageUrl");
            }
        }

        public ICommand NextImageCommand => new RelayCommand(NextImage);

        private void NextImage()
        {
            ImageCounter++;
            if (ImageCounter > _images.Url.Count-1)
                ImageCounter = 0;
            _imageUrl = _images.Url[ImageCounter];
            OnPropertyChanged(nameof(ImageUrl));
        }
        public ICommand PreviousImageCommand => new RelayCommand(PreviousImage);
        private void PreviousImage()
        {
            ImageCounter--;
            if (ImageCounter < 0)
                ImageCounter = _images.Url.Count-1;
            _imageUrl = _images.Url[ImageCounter];
            OnPropertyChanged(nameof(ImageUrl));
        }

        public ICommand GoBackCommand => new RelayCommand(GoBack);
        private void GoBack()
        {
            Navigator.NavigateTo(new MyAccommodationsListView(_logedInUser, Navigator));
        }
    }
}
