using InitialProject.Domain.Model;
using InitialProject.Forms;
using InitialProject.Repository;
using InitialProject.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using InitialProject.View.OwnerView;

namespace InitialProject
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {
        public bool alreadyStartedTourFlag { get; set; }
        private readonly UserRepository _repository;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            alreadyStartedTourFlag = false;
            _repository = new UserRepository();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _repository.GetByUsername(Username);
            if (user != null)
            {
                if(user.Password == txtPassword.Password)
                {
                    if(user.Role == UserRole.Owner)
                    {
                        MainWindowOwner mainWindowOwner = new MainWindowOwner(user);
                        //OwnerMainWindow ownerMainWindow = new OwnerMainWindow(user);
                        mainWindowOwner.Show();
                    }
                    if(user.Role == UserRole.Guest)
                    {
                        AccommodationShow accommodationShow = new AccommodationShow(user);
                        accommodationShow.Show();
                    }
                    if(user.Role == UserRole.Guide)
                    {
                        GuideMainWindow guideMainWindow = new GuideMainWindow(user);  
                        guideMainWindow.Show();
                    }
                    if (user.Role == UserRole.Guest2)
                    {
                        ShowTour showTour = new ShowTour(user);
                        showTour.Show();
                    }
                    Close();
                } 
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }
            
        }
    }
}
