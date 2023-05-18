using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InitialProject.Utilities;

namespace InitialProject.ViewModels.SettingsViewModel
{
    public class OwnerChooseLanguageViewModel : BaseViewModel
    {
        public OwnerChooseLanguageViewModel() { }


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
            }

            if (IsEnglishSelected)
            {
                app.ChangeLanguage("en-US");
            }

            CloseCurrentWindow();
        }
    }
}
