using InitialProject.Domain.Model.Users;
using InitialProject.Service.SettingsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InitialProject.Domain.Model;
using InitialProject.Utilities;

namespace InitialProject.ViewModels.SettingsViewModel
{
    public class OwnerChooseThemeViewModel: BaseViewModel
    {
        private readonly OwnerSettingsService _ownerSettingsService;
        private readonly User _logedInUser;
        public OwnerChooseThemeViewModel(User logedInUser)
        {
            _ownerSettingsService = new OwnerSettingsService();
            _logedInUser = logedInUser;
            OwnerSettings settings = _ownerSettingsService.GetByOwnerId(logedInUser.Id);
            if (settings.Theme == "dark")
                _isDarkSelected = true;
            else
                _isLightSelected = true;
        }


        private bool _isDarkSelected;
        public bool IsDarkSelected
        {
            get { return _isDarkSelected; }
            set
            {
                _isDarkSelected = value;
                OnPropertyChanged("IsDarkSelected");
            }
        }

        private bool _isLightSelected;
        public bool IsLightSelected
        {
            get { return _isLightSelected; }
            set
            {
                _isLightSelected = value;
                OnPropertyChanged("IsLightSelected");
            }
        }

        public ICommand ChangeThemeCommand => new RelayCommand(ChangeTheme);

        private void ChangeTheme()
        {
            ResourceDictionary themeDictionary = new ResourceDictionary();
            var app = (App)Application.Current;
            if (IsDarkSelected)
            {
                ResourceDictionary lightThemeDictionary = App.Current.Resources.MergedDictionaries.FirstOrDefault(d => d.Source.OriginalString == "LightTheme.xaml");
                App.Current.Resources.MergedDictionaries.Remove(lightThemeDictionary);
                themeDictionary.Source = new Uri("../../Themes/DarkTheme.xaml", UriKind.Relative);
                _ownerSettingsService.UpdateTheme(_logedInUser.Id, "dark");
            }

            if (IsLightSelected)
            {
                ResourceDictionary darkThemeDictionary = App.Current.Resources.MergedDictionaries.FirstOrDefault(d => d.Source.OriginalString == "DarkTheme.xaml");
                App.Current.Resources.MergedDictionaries.Remove(darkThemeDictionary);
                themeDictionary.Source = new Uri("../../Themes/LightTheme.xaml", UriKind.Relative);
                _ownerSettingsService.UpdateTheme(_logedInUser.Id, "light");
            }
            App.Current.Resources.MergedDictionaries.Add(themeDictionary);
            CloseCurrentWindow();
        }
    }
}
