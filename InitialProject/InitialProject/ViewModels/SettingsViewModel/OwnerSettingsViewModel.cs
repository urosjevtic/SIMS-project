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

        private readonly User _logedInUser;
        public OwnerSettingsViewModel(User logedInUser, NavigationService navigationService)
        {
            _logedInUser = logedInUser;
        }

        public ICommand ChangeLanguageCommand => new RelayCommand(ChangeLanguage);

        private void ChangeLanguage()
        {
            OwnerChooseLanguageView chooseLanguageView = new OwnerChooseLanguageView(_logedInUser);
            chooseLanguageView.Show();
        }

        public ICommand ChangeThemeCommand => new RelayCommand(ChangeTheme);

        private void ChangeTheme()
        {
            OwnerChooseThemeView chooseLanguageView = new OwnerChooseThemeView(_logedInUser);
            chooseLanguageView.Show();
        }
    }
}
