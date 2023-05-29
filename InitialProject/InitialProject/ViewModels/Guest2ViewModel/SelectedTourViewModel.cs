using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Service;
using InitialProject.View.Guest2View;

namespace InitialProject.ViewModels.Guest2ViewModel
{
    public class SelectedTourViewModel : BaseViewModel
    {
        public Tour selectedTour { get; set; }
        public User LoggedUser { get; set; }
        public ICommand ReserveCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public List<string> Images
        {
            get => selectedTour.CoverImageUrl.Url;
            set
            {
                if (selectedTour.CoverImageUrl.Url == value) return;
                selectedTour.CoverImageUrl.Url = value;
                OnPropertyChanged(nameof(Images));
            }
        }
        public List<string> Image
        {
            get => Image;
            set
            {
                if (Image == value) return;
                Image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
        public List<string> Checkpoints { get; set; }
        public ImageService _imageService;
        public CheckPointService _checkPointService;
        public NavigationService navService { get; }
        public SelectedTourViewModel(Tour t, NavigationService nav)
        {
            selectedTour = t;
            this.navService = nav;
            _imageService = new ImageService();
            _checkPointService = new CheckPointService();
            GoBackCommand = new RelayCommand(GoBack);
            ReserveCommand = new RelayCommand(Reserve);
            Images = _imageService.GetAllById(selectedTour);
            Checkpoints = FindCheckPointNames();
        }
        private void GoBack()
        {
            if (navService.CanGoBack)
            {
                navService.GoBack();
            }
        }
        public void Reserve()
        {
            navService.Navigate(new SearchResultPage(selectedTour,navService));
        }
        public List<string> FindCheckPointNames()
        {
            List<string> result = new List<string>();
            foreach(CheckPoint ch in selectedTour.CheckPoints)
            {
                result.Add(ch.Name);
            }
            return result;
        }
    }
}
