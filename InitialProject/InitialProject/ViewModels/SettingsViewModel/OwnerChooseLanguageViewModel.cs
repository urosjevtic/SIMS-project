using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Users;
using InitialProject.Service.SettingsService;
using InitialProject.Utilities;

namespace InitialProject.ViewModels.SettingsViewModel
{
    public class OwnerChooseLanguageViewModel : BaseViewModel
    {

        private readonly OwnerSettingsService _ownerSettingsService;
        private readonly User _logedInUser;
        public OwnerChooseLanguageViewModel(User logedInUser)
        {
            _ownerSettingsService = new OwnerSettingsService();
            _logedInUser = logedInUser;
            OwnerSettings settings = _ownerSettingsService.GetByOwnerId(logedInUser.Id);
            if (settings.Language == "sr-LATN")
                _isSerbianSelected = true;
            else
                _isEnglishSelected = true;
        }


        private bool _isSerbianSelected;
        public bool IsSerbianSelected
        {
            get { return _isSerbianSelected; }
            set
            {
                _isSerbianSelected = value;
                OnPropertyChanged("IsSerbianSelected");
            }
        }

        private bool _isEnglishSelected;
        public bool IsEnglishSelected
        {
            get { return _isEnglishSelected; }
            set
            {
                _isEnglishSelected = value;
                OnPropertyChanged("IsEnglishSelected");
            }
        }

        public ICommand ChangeLanguageCommand => new RelayCommand(ChangeLanguage);

        private void ChangeLanguage()
        {
            var app = (App)Application.Current;
            if (IsSerbianSelected)
            {
                app.ChangeLanguage("sr-LATN");
                _ownerSettingsService.UpdateLanguage(_logedInUser.Id, "sr-LATN");
            }

            if (IsEnglishSelected)
            {
                app.ChangeLanguage("en-US");
                _ownerSettingsService.UpdateLanguage(_logedInUser.Id, "en-US");
            }

            CloseCurrentWindow();
        }
    }
}
