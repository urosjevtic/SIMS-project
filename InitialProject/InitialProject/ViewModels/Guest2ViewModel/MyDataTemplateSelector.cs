using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using InitialProject.Domain.Model;

namespace InitialProject.ViewModels.Guest2ViewModel
{
    public class MyDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OkTemplate { get; set; }
        public DataTemplate YesNoTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {

            if (item is TourNotification yourItem)
            {
                if (yourItem.notification.CheckPointId == -1)
                {
                    return OkTemplate;
                }
                else if (yourItem.notification.CheckPointId >= 0)
                {
                    return YesNoTemplate;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}
