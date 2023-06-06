﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InitialProject.Utilities;

namespace InitialProject.ViewModels.PopupViewModel
{
    public class SuccessfulRenovationViewModel : BaseViewModel
    {
        public SuccessfulRenovationViewModel() { }

        public ICommand OkCommand => new RelayCommand(CloseWindow);

        private void CloseWindow()
        {
            CloseCurrentWindow();
        }
    }
}
