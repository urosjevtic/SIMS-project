using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.View.OwnerView
{
    /// <summary>
    /// Interaction logic for Rating.xaml
    /// </summary>
    public partial class Rating : UserControl
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(int), typeof(Rating),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged));

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Rating ratingControl = (Rating)d;
            int newValue = (int)e.NewValue;

            UIElementCollection children = ((Grid)ratingControl.Content).Children;

            for (int i = 0; i < children.Count; i++)
            {
                ToggleButton button = children[i] as ToggleButton;
                if (button != null)
                    button.IsChecked = (i < newValue);
            }
        }

        private void ClickEventHandler(object sender, RoutedEventArgs e)
        {
            ToggleButton button = sender as ToggleButton;
            int newValue = int.Parse(button.Tag.ToString());
            Value = newValue;
        }

        public Rating()
        {
            InitializeComponent();
        }
    }
}
