using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InitialProject.Domain.Model;
using InitialProject.Utilities;
using InitialProject.View.GuideView;
using InitialProject.View.GuideView.Pages;
using InitialProject.Service;

namespace InitialProject.ViewModels.MainVeiwModel
{
    public class GuideMainViewModel
    {
        private readonly TourService _tourService;  
        public MainWindow _guideMainWindow { get; set; }
        public User LoggedInUser { get; set; }
        public ICommand NavigationButtonCommand => new RelayCommandWithParams(Navigationations);

        public List<Tour> ActiveTours { get; set; } 
        public GuideMainViewModel(User user, MainWindow guideMain)
        {
            _tourService = new TourService();
            _guideMainWindow = guideMain;
            LoggedInUser = user;
            ActiveTours = _tourService.FindActiveTours(user);

        }
        public void Navigationations(object param)
        {
            string nextPage = param.ToString();
            var navigationService = _guideMainWindow.MainFrame.NavigationService;

            switch (nextPage)
            {
                case "MyTours":
                    navigationService.Navigate(new MyTours(LoggedInUser));
                    break;
                case "TodayTours":
                    navigationService.Navigate(new TodayToursPage(LoggedInUser,_guideMainWindow));
                    break;
                case "ActiveTour":
                    if (ActiveTours.Count == 0 || _tourService.FindActiveTours(LoggedInUser).Count == 0)
                    {
                        navigationService.Navigate(new NoActiveTours());
                        break;
                    }
                    else
                    {
                        navigationService.Navigate(new ActiveTourPage(LoggedInUser));
                        break;
                    }
                case "Statistic":
                    navigationService.Navigate(new StatisticsPage(LoggedInUser));
                    break;

                case "Requests":
                    navigationService.Navigate(new Requests(LoggedInUser));
                    break;

            }
        }
    }
}
