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
using InitialProject.ViewModels;
using InitialProject.Domain.Model;
using InitialProject.Service;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class OneTourStatistic : Window
        
    {
        public TourStatisticService _tourStatisticService;
        public TourStatisticViewModel TourStatisticViewModel;
        public Tour SelectedTour { get; set; }
        public OneTourStatistic(Tour tour)
        {
            InitializeComponent();
            TourStatisticViewModel = new TourStatisticViewModel();
            this.DataContext = TourStatisticViewModel;
            _tourStatisticService = new TourStatisticService();
            SelectedTour = tour;
            until18TextBlock.Text = _tourStatisticService.FindPeopleUntil18(SelectedTour).ToString();
            between18and50TextBlock.Text = _tourStatisticService.FindPeopleBetween18and50(SelectedTour).ToString();
            over50TextBlock.Text = _tourStatisticService.FindPeopleOver50(SelectedTour).ToString();
            withoutVoucherTextBlock.Text = _tourStatisticService.FindPeopleWithoutVoucher(SelectedTour).ToString() + "%";
            withVoucherTextBlock.Text = _tourStatisticService.FindPeopleWithVoucher(SelectedTour).ToString() + "%";
        }

    }
}
