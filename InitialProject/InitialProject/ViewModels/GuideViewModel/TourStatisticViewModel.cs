using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Service;
using InitialProject.Domain.Model;
using InitialProject.View;
using InitialProject.Utilities;
using System.Windows.Input;
using InitialProject.View.GuideView;
using System.Windows.Controls;

namespace InitialProject.ViewModels
{
    public class TourStatisticViewModel : BaseViewModel
    {
        public TourStatisticService _tourStatisticService;
        public TourService _tourService; 
        public List<Tour> EndedTours { get; set; }  
        public Tour SelectedTour { get; set; }  
        public Tour MostVisitedTour { get; set; }
        public string MostVisitedTourInYearText { get; set; }   
        public ComboBox ComboBox { get; set; }  
        public TextBlock TextBlock { get; set; }
        public string MostVisitedInYear { get; set; }

        public TourStatisticViewModel(ComboBox comboBox,TextBlock textBlock)
        {
            _tourService = new TourService();
            _tourStatisticService = new TourStatisticService();
            EndedTours = _tourService.FindAllEndedTours();
            MostVisitedTour = _tourService.FindMostVisited();
           
            ComboBox = comboBox;
            TextBlock = textBlock;
            
            ShowVisitation = new RelayCommand(ShowVisitatons);
            MostVisitedTourInYear();
        }
        public ICommand ShowVisitation { get; private set; }


        public void ShowVisitatons()
        {
            Visitation visitation = new Visitation(SelectedTour);
            visitation.Show();
        }
       
       

        private void MostVisitedTourInYear()
        {
            if (ComboBox.SelectedItem != null)
            {
                MostVisitedInYear = _tourService.GetMostVisitedInYear(GetSelectedYear(ComboBox).ToString()).ToString();
            }
        }

        private int GetSelectedYear(ComboBox comboBox)
        {
            if (comboBox.SelectedItem == comboBox.Items[0]) { return 2023; }
            else if (comboBox.SelectedItem == comboBox.Items[1]) { return 2022; }
            else return 2021;

        }
    }
}
