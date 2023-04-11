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
using InitialProject.Repository;
using System.Data;
using InitialProject.Service;

using InitialProject.Domain.Model;
using InitialProject.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InitialProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for TourSearch.xaml
    /// </summary>
    public partial class TourSearch : Window
    {
        public TourSearchViewModel TourSearchViewModel { get; set; }
        public User LoggedUser { get; set; }

        public TourSearch(User user)
        {
            InitializeComponent();
            LoggedUser = user;
            TourSearchViewModel = new TourSearchViewModel(LoggedUser);
            this.DataContext = TourSearchViewModel;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
