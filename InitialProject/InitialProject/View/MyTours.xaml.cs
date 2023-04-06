using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using GalaSoft.MvvmLight.Command;
using InitialProject.Forms;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for MyTours.xaml
    /// </summary>
    public partial class MyTours : Window
    {
        private readonly VoucherRepository _voucherRepository;
        public CheckPointService _checkPointService;
        public TourService _tourService;
        public Tour SelectedTour { get; set; }
        public List<Tour> Tours { get; set; }
        public User LoggedUser { get; set; } 
        public MyTours(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            LoggedUser = user;
            _tourService = new TourService();
            _checkPointService = new CheckPointService();
            Tours = _tourService.FindAllActiveTours();
            activeTours.ItemsSource = new ObservableCollection<Tour>(Tours);
        }
        private void GoBackButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ViewCheckpointsButton(object sender, RoutedEventArgs e)
        {
            SelectedTour = (Tour)((Button)sender).DataContext;
            // Set the SelectedTour property to the selected tour item
            List<CheckPoint> CheckPoints = SelectedTour.CheckPoints;
            listBox.ItemsSource = new ObservableCollection<CheckPoint>(CheckPoints);
        }

        private void SubmitRateButton(object sender, RoutedEventArgs e)
        {

        }

        private void OpenCommentFormButton(object sender, RoutedEventArgs e)
        {
            CommentForm commentForm = new(LoggedUser);
            commentForm.Show();
        }
    }
}
