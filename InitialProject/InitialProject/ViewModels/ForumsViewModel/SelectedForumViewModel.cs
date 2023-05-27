using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Forums;
using InitialProject.Service.ForumServices;
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
            ForumComments = new ObservableCollection<ForumComment>(SelectedForum.Comments);
        }


    }
}
