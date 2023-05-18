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
using InitialProject.Serializer;
using InitialProject.Repository;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using InitialProject.ViewModel;
using InitialProject.ViewModels;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using System.Collections;
using InitialProject.Utilities;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AddingTour.xaml
    /// </summary>
    public partial class AddingTour : Window
    {   
        public AddingTour(User user,ShortTourRequest request, bool enter)
        {
            InitializeComponent();        
            this.DataContext = new AddingTourViewModel(user,request, enter);
        }

     
    }
}
