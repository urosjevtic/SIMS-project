using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using ceTe.DynamicPDF;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Forums;
using InitialProject.Service;
using InitialProject.Service.ForumServices;
using InitialProject.Service.NotesServices;
using InitialProject.Service.RatingServices;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.Forums;
using InitialProject.View.OwnerView.MyAccommodations;

namespace InitialProject.ViewModels.ForumsViewModel
{
    public class SelectedForumViewModel : BaseViewModel
    {
        private readonly User _logedInUser;
        public Forum SelectedForum { get; set; }
        private readonly ForumService _forumService;
        public NavigationService NavigationService { get; set; }
        public ObservableCollection<ForumComment> ForumComments { get; set; }

        public SelectedForumViewModel(User logedInUser, NavigationService navigationService, Forum selectedForum)
        {
            _logedInUser = logedInUser;
            SelectedForum = selectedForum;
            _forumService = new ForumService();
            NavigationService = navigationService;

            _forumService.BindCommentsToForum(SelectedForum);
            _forumService.CheckWhatCommentsAreReported(_logedInUser, SelectedForum.Comments);

            _canOwnerComment = _forumService.CanOwnerMakeComment(selectedForum, _logedInUser.Id);

            ForumComments = new ObservableCollection<ForumComment>();
            foreach (var comment in SelectedForum.Comments)
            {
                _forumService.CheckUserRole(selectedForum, comment);
                ForumComments.Add(comment);
            }
        }



        private string _newComment;

        public string NewComment
        {
            get { return _newComment;}
            set
            {
                _newComment = value;
                OnPropertyChanged("NewComment");
            }
        }



        public ICommand MakeCommentCommand => new RelayCommand(MakeComment);

        private void MakeComment()
        {
            ForumComment newComment = _forumService.AddNewComment(SelectedForum, _logedInUser, _newComment);
            ForumComments.Add(newComment);
            _newComment = "";
        }


        public ICommand GoBackCommand => new RelayCommand(GoBack);

        private void GoBack()
        {
            NavigationService.Navigate(new ForumSelcetionView(_logedInUser, NavigationService));
        }

        

        private bool _canOwnerComment;
        public bool CanOwnerComment
        {
            get { return _canOwnerComment; }
            set
            {
                if (_canOwnerComment != value)
                {
                    _canOwnerComment = value;
                    OnPropertyChanged(nameof(CanOwnerComment));
                }
            }
        }

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
        public ICommand ReportCommentCommand => new RelayCommandWithParams(ReportComment);



        private void ReportComment(object parameter)
        {
            if (parameter is ForumComment selectedComment)
            {

                if (!selectedComment.HasUserReported)
                {
                    _forumService.ReportComment(selectedComment, _logedInUser);
                    selectedComment.HasUserReported = true;
                }
                else
                {
                    _forumService.RemoveCommentReport(selectedComment, _logedInUser);
                    selectedComment.HasUserReported = false;
                }

            }
        }
    }
}
