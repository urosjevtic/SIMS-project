using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using InitialProject.Domain.Model;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.Settings;

namespace InitialProject.ViewModels.SettingsViewModel
{
    public class OwnerSettingsViewModel
    {


        public OwnerSettingsViewModel(User logedInUser, NavigationService navigationService)
        {

        }

        public ICommand ChangeLanguageCommand => new RelayCommand(ChangeLanguage);

        private void ChangeLanguage()
        {
            OwnerChooseLanguageView chooseLanguageView = new OwnerChooseLanguageView();
            chooseLanguageView.Show();
        }
    }
}
