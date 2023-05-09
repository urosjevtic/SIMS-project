using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        public SelectedTourViewModel(Tour t, User loggedUser)
        {
            selectedTour = t;
            LoggedUser = loggedUser;
            _imageService = new ImageService();
            _checkPointService = new CheckPointService();
            GoBackCommand = new RelayCommand(CloseCurrentWindow);
            ReserveCommand = new RelayCommand<Tour>(Reserve);
            Images = _imageService.GetAllById(selectedTour);
            Checkpoints = FindCheckPointNames();
        }
        public void Reserve(Tour tour)
        {
            SearchResult searchResult = new SearchResult(LoggedUser, selectedTour);
            CloseCurrentWindow();
            searchResult.Show();
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
