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
using System.Windows.Shapes;
using InitialProject.Model;
using InitialProject.Serializer;
using InitialProject.Repository;
using System.Collections.ObjectModel;
using InitialProject.ViewModel;


namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GuideMainWindow : Window
    {
        public User LoggedUser { get; set; }
        

        public GuideMainViewModel GuideMainViewModel { get; set; }
        public GuideMainWindow(User user)
        {
            InitializeComponent();
            GuideMainViewModel = new GuideMainViewModel(user);
            this.DataContext = GuideMainViewModel;
            LoggedUser = user;

            ToursDataGrid.ItemsSource = GuideMainViewModel.UpdateToursDataGrid();
            TodayToursDataGrid.ItemsSource = GuideMainViewModel.UpdateTodayToursDataGrid();
            ActiveToursDataGrid.ItemsSource = new ObservableCollection<Tour>(GuideMainViewModel.ActiveTours);
            GuideMainViewModel.LoadData();
        }

        
        private void AddTourClick(object sender, RoutedEventArgs e)
        {
            GuideMainViewModel.AddTour();
        }

        
        public void StartTourClick(object sender, RoutedEventArgs e)
        {
            GuideMainViewModel.StartTour();
        }

        private void ShowTourClick(object sender, RoutedEventArgs e)
        {
            GuideMainViewModel.ShowTour();
        }

        private void CancelTourClick(object sender, RoutedEventArgs e)
        {
            GuideMainViewModel.CancelTour();
        }
    }
}
    

