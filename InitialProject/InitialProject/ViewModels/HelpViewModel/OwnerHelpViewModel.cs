using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace InitialProject.ViewModels.HelpViewModel
{
    public class OwnerHelpViewModel : BaseViewModel
    {
        public NavigationService NavigationService { get; set; }
        public OwnerHelpViewModel(NavigationService navigationService)
        {
            NavigationService = navigationService;
        }
    }
}
