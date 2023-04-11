using InitialProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.ViewModels
{
    public abstract class SideScreenViewModel : BaseViewModel
    {
        public ICommand MyAccommoadionsOpenCommand => new RelayCommand(MyAccommoadionsOpen);
        public ICommand RatingsOpenCommand => new RelayCommand(RatingsOpen);

        public ICommand ReservationsOpenCommand => new RelayCommand(ReservationsOpen);


        protected abstract void MyAccommoadionsOpen();
        protected abstract void RatingsOpen();

        protected abstract void ReservationsOpen();
    }
}
