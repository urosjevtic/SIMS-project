using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using InitialProject.Domain.Model;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.ViewModels;

namespace InitialProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for SearchResult.xaml
    /// </summary>
    public partial class SearchResult : Window
    {
        public SearchResultViewModel SearchResultViewModel { get; set; }
        public User LoggedUser { get; set; }
        public SearchResult(User user, Tour tour)
        {
            InitializeComponent();
            LoggedUser = user;
            SearchResultViewModel = new SearchResultViewModel(LoggedUser, tour);
            this.DataContext = SearchResultViewModel;
        }
    }
}

