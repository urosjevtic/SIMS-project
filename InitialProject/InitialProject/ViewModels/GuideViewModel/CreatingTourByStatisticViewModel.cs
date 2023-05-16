using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InitialProject.Service;
using InitialProject.Utilities;

namespace InitialProject.ViewModels.GuideViewModel
{
    public class CreatingTourByStatisticViewModel
    {

        private ShortTourRequestService _shortTourRequestService;
        public string MostWantedLanguage { get; set; }
        public string MostWantedLocation { get; set; }
        public ICommand AddLanguageCommand {get; private set;}
        public CreatingTourByStatisticViewModel()
        {
            _shortTourRequestService = new ShortTourRequestService();

            MostWantedLanguage = FindMostWantedLanguage();
            MostWantedLocation = FindMostWantedLocation();
            AddLanguageCommand = new RelayCommand(AddLanguage);
        }

        private string FindMostWantedLocation()
        {
            return "Location : " + _shortTourRequestService.GetMostWantedLocation().ToString();
        }
        private string FindMostWantedLanguage()
        {
            return "Language: " + _shortTourRequestService.GetMostWantedLanguage().ToString();
        }

        private void AddLanguage()
        {

        }
    }
}
