using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using InitialProject.Domain.Model;
using InitialProject.Utilities;

namespace InitialProject.ViewModels
{
    public class MainWindowViewModel
    {
        private NavigationService _navigationService;

        public ICommand NavigateCommand => new RelayCommand<string>(OnNavigateTo);
        public ICommand NavigateBackCommand => new RelayCommand(OnNavigateBack);
        public ICommand ShowWizardCommand => new RelayCommand(OnShowWizard);
        public ICommand SaveSettingsCommand => new RelayCommand(OnSaveSettings);

        public User LoggedUser { get; set; } = App.LoggedUser;      

        public MainWindowViewModel()
        {
            
        }

        public MainWindowViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private void OnNavigateTo(string destinationPage)
        {
            string destinationPagePath = "View/" + destinationPage + ".xaml";
            _navigationService.Navigate(new Uri(destinationPagePath, UriKind.Relative));
        }

        private void OnNavigateBack()
        {
            if (_navigationService.CanGoBack)
            {
                _navigationService.GoBack();
            }
        }

        private void OnShowWizard()
        {
            if (Properties.Settings.Default.ShowWizard)
            {
                WizardWindow wizardWindow = new WizardWindow();
                wizardWindow.ShowDialog();
            }
        }

        private void OnSaveSettings()
        {
            Properties.Settings.Default.Save();
        }
    }
}
