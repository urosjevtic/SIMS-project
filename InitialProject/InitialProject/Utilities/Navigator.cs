using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InitialProject.Utilities
{
    public class Navigator
    {
        private readonly Frame _frame;

        public Navigator(Frame frame)
        {
            _frame = frame;
        }

        public void NavigateTo(Page page)
        {
            _frame.Navigate(page);
        }
    }
}
