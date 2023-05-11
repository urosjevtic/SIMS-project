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
using System.Collections.ObjectModel;
using InitialProject.ViewModel;
using InitialProject.Domain.Model;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GuideMainWindow : Window
    {

        public GuideMainWindow(User user)
        {
            InitializeComponent();
           // this.DataContext =  new GuideMainViewModel(user);
           
        }

      
    }
}
    

