using GalaSoft.MvvmLight.Views;
using InitialProject.Domain.Model.Forums;
using InitialProject.ViewModels;
using InitialProject.ViewModels.ForumsViewModel;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for SelectedForumPage.xaml
    /// </summary>
    public partial class SelectedForumPage : Page
    {
        public SelectedForumPage(Forum selectedForum, NavigationService navigationService)
        {
            InitializeComponent();
            DataContext = new ForumSelectedViewModel(selectedForum, navigationService);
        }

        //private void Page_Loaded(object sender, RoutedEventArgs e)
        //{
        //    this.DataContext = new ForumSelectedViewModel(selectedForum,this.NavigationService);
        //}
    }
}
