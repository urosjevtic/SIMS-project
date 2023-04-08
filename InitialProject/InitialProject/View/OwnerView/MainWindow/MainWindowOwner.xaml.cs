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
using InitialProject.Domain.Model;
using InitialProject.ViewModel;
using InitialProject.ViewModels;

namespace InitialProject.View.OwnerView
{
    /// <summary>
    /// Interaction logic for MainWindowOwner.xaml
    /// </summary>
    public partial class MainWindowOwner : Window
    {
        public OwnerMainViewModel OwnerMainViewModel { get; }

        public MainWindowOwner(User user)
        {
            InitializeComponent();

            OwnerMainViewModel = new OwnerMainViewModel(user);
            DataContext = OwnerMainViewModel;
        }
    }
}
