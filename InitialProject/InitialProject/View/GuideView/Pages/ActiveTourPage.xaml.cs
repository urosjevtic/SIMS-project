using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InitialProject.Domain.Model;
using InitialProject.ViewModels.GuideViewModel;
using System.Collections.ObjectModel;

namespace InitialProject.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for ActiveTourPage.xaml
    /// </summary>
    public partial class ActiveTourPage : Page
    {
        ActiveTourViewModel _activeTourViewModel { get; set; }

        public ActiveTourPage(User user)
        {
            InitializeComponent();
            _activeTourViewModel = new ActiveTourViewModel(user);
            DataContext = _activeTourViewModel; 
        }

        private void CheckBoxChecked(object sender, RoutedEventArgs e)
        {
            _activeTourViewModel. CheckPoints = new ObservableCollection<CheckPoint>(_activeTourViewModel.ActiveTour.CheckPoints);
            CheckPoint checkedCheckPoint = ((CheckBox)sender).DataContext as CheckPoint;
            _activeTourViewModel.CheckCheckPoint(checkedCheckPoint);
            _activeTourViewModel.SendNotification(checkedCheckPoint);
            if (_activeTourViewModel.CheckPoints.Last().Id == checkedCheckPoint.Id)
            {
                _activeTourViewModel.EndTour();
            }
        }
    }
}
