using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InitialProject.Utilities;

namespace InitialProject.ViewModels
{
    public class WizardViewModel
    {
        public ICommand SaveWizardSettingsCommand => new RelayCommand(OnSaveWizardSettings);

        private void OnSaveWizardSettings()
        {
            Properties.Settings.Default.ShowWizard = false;
            Properties.Settings.Default.Save();
        }
    }
}
