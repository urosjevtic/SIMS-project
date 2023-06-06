using GalaSoft.MvvmLight.Messaging;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Forums;
using InitialProject.Service.ForumServices;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.Forums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Notification.Wpf;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace InitialProject.ViewModels
{
    public class ForumSelectedViewModel : BaseViewModel
    {
        public ForumSelectedViewModel(Forum selectedForum, NavigationService navigationService)
        {
            SelectedForum = selectedForum;
            _forumService = new ForumService();
            NavigationService = navigationService;

            _forumService.BindCommentsToForum(selectedForum);
            _forumService.CheckWhatCommentsAreReported(_logedInUser, SelectedForum.Comments);

            ForumComments = new ObservableCollection<ForumComment>();
            foreach (var comment in SelectedForum.Comments)
            {
                _forumService.CheckUserRole(selectedForum, comment);
                ForumComments.Add(comment);
            }
        }

        private readonly User _logedInUser=App.LoggedUser;
        private Forum _selectedForum;
        public Forum SelectedForum
        {
            get
            {
                return _selectedForum;
            }
            set
            {
                if (value != _selectedForum)
                {
                    _selectedForum = value;
                    OnPropertyChanged(nameof(SelectedForum));
                }
            }
        }
        private readonly ForumService _forumService;
        public NavigationService NavigationService { get; set; }
        public ObservableCollection<ForumComment> ForumComments { get; set; }



        private string _newComment;

        public string NewComment
        {
            get { return _newComment; }
            set
            {
                _newComment = value;
                OnPropertyChanged("NewComment");
            }
        }

       
       

        public ICommand CloseForumCommand => new RelayCommand(CloseForum);

        private void CloseForum()
        {
            SelectedForum.IsOpen = false;
            // pozovi servis da sacuvas forum sada kada je njegov status izmenjen - Sacuvas SelectedForum
            _forumService.ChangeStatus(SelectedForum, SelectedForum.IsOpen);
            Messenger.Default.Send<ToastNotification>(new ToastNotification("Success", "You have successfully closed the forum.", NotificationType.Success));
        }

        public ICommand MakeCommentCommand => new RelayCommand(MakeComment);

        private void MakeComment()
        {
            ForumComment newComment = _forumService.AddNewComment(SelectedForum, _logedInUser, _newComment);
            ForumComments.Add(newComment);
            _newComment = "";
        }


        //public ICommand GoBackCommand => new RelayCommand(GoBack);

        //private void GoBack()
        //{
        //    NavigationService.Navigate(new ForumSelcetionView(_logedInUser, NavigationService));
        //}


        private bool _hasUserReported;
        public bool HasUserReported
        {
            get { return _hasUserReported; }
            set
            {
                if (_hasUserReported != value)
                {
                    _hasUserReported = value;
                    OnPropertyChanged(nameof(HasUserReported));
                }
            }
        }

        //public ICommand ReportCommentCommand => new RelayCommandWithParams(ReportComment);

        //private void ReportComment(object parameter)
        //{
        //    if (parameter is ForumComment selectedComment)
        //    {

        //        if (!selectedComment.HasUserReported)
        //        {
        //            _forumService.ReportComment(selectedComment, _logedInUser);
        //            selectedComment.HasUserReported = true;
        //        }
        //        else
        //        {
        //            _forumService.RemoveCommentReport(selectedComment, _logedInUser);
        //            selectedComment.HasUserReported = false;
        //        }
        //    }
        //}
    }
}

