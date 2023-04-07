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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using InitialProject.ViewModel;
using InitialProject.ViewModels;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AddingTour.xaml
    /// </summary>
    public partial class AddingTour : Window
    {   
        public AddingTourViewModel AddingTourViewModel { get; set; }    

        
        public User LoggedUser { get; set; }
        public AddingTour(User user)

        {
            InitializeComponent();
            LoggedUser = user;
            AddingTourViewModel = new AddingTourViewModel(LoggedUser);
            this.DataContext = AddingTourViewModel;            
        }


        private void SaveClick(object sender, RoutedEventArgs e)
        {
            AddingTourViewModel.ConfirmAddingTour();
            this.Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
