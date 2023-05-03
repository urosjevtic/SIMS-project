using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InitialProject.Utilities;

namespace InitialProject.ViewModels
{
    public class MainWindowViewModel
    {
        public ICommand ShowWizardCommand => new RelayCommand(ShowWizard);
        public ICommand SaveSettingsCommand => new RelayCommand(SaveSettings);

        private void ShowWizard()
        {
            if (Properties.Settings.Default.ShowWizard)
            {
                WizardWindow wizardWindow = new WizardWindow();
                wizardWindow.ShowDialog();
            }
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }
    }
}
