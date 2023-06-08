using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InitialProject.Domain.Model;
using InitialProject.Service;
using InitialProject.View;
using InitialProject.Utilities;
using System.Windows;

namespace InitialProject.ViewModels.GuideViewModel
{
    public class ComplexRequestsViewModel
    {
        public List<ShortTourRequest> Requests { get; set; }    
        public ShortTourRequest SelectedRequest { get; set; }  
        public ICommand AcceptRequestCommand { get; private set; }
        public User LoggedUser { get; set; }    
        public ComplexRequestsViewModel(User user,List<ShortTourRequest> requests)
        {
           Requests = requests;
            AcceptRequestCommand = new RelayCommand(AcceptRequest);
            LoggedUser = user;
        }
        private void AcceptRequest()
        {
            if (SelectedRequest != null)
            {
                AddingTour addingTourWindow = new AddingTour(LoggedUser, SelectedRequest, false);
                addingTourWindow.Show();
            }
            else
            {
                MessageBox.Show("Izaberite zahtjev koji zelite da prikaze");
            }
        }
    }
}
