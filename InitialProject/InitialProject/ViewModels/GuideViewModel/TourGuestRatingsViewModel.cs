using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Service;
using InitialProject.Repository;
using InitialProject.Model;
using InitialProject.Utilities;
using System.Windows.Input;
using System.Windows;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.ViewModels;

namespace InitialProject.ViewModels
{
    public class TourGuestRatingsViewModel : BaseViewModel
    {

        public List<Comment> Comments { get; set; }
        public List<RatedGuideTour> TourGuests { get; set; }
        public List<TourGuest> Guests { get; set; }
        public Tour Tour { get; set; }
        public List<GuestsCheckPoint> GuestsCheckPoints { get; set; }
        public RatedGuideTourService _ratedTourGuideService;
        private readonly CommentRepository _commentRepository;
        private readonly ITourGuestRepository _tourGuestRepository;
        private readonly IGuestsCheckPointRepository _guestsCheckPointRepository;
        private UserService _userService;
        public RelayCommandWithParams ReportCommand { get; set; }
        public List<GuideCommentsOverview> GuideCommentsOverview { get; set; }
       // public GuideCommentsOverview SelectedComment{get; set;}

        private GuideCommentsOverview _selectedComment;
        public GuideCommentsOverview SelectedComment
        {
            get { return _selectedComment; }
            set
            {
                if (_selectedComment != value)
                {
                    _selectedComment = value;
                    OnPropertyChanged(nameof(SelectedComment));
                }
            }
        }

        public TourGuestRatingsViewModel(Tour tour)
        {
            _userService = new UserService();
            _commentRepository = new CommentRepository();
            _ratedTourGuideService = new RatedGuideTourService();
            _tourGuestRepository = Injector.Injector.CreateInstance<ITourGuestRepository>();
            _guestsCheckPointRepository = Injector.Injector.CreateInstance<IGuestsCheckPointRepository>();

            GuestsCheckPoints = new List<GuestsCheckPoint>();
            TourGuests = _ratedTourGuideService.FindAllTourRatings(tour);
            Comments = _commentRepository.GetByTour(tour);                       
            Guests = _tourGuestRepository.GetAll();                              
            Tour = tour;
            GuideCommentsOverview = GetAllGuideComments();

            ReportCommand = new RelayCommandWithParams(ExecuteReporting);

        }

        public void ExecuteReporting(object sender)
        {
           if(SelectedComment != null)
            {
                SelectedComment.IsReported = true;
                Comment reported = _commentRepository.GetByText(SelectedComment.Comment);
                reported.IsReported = true;
                _commentRepository.Update(reported);
                MessageBox.Show("Komentar je prijavljen!","Information",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

     
      private List<GuideCommentsOverview> GetAllGuideComments()
        {
            List<GuideCommentsOverview> guideCommentsOverview = new List<GuideCommentsOverview>();
            foreach (Comment comment in Comments)
            {
                foreach(GuestsCheckPoint guestsCheckPoint in _guestsCheckPointRepository.GetAll())
                {
                    if(comment.User.Id == guestsCheckPoint.Guest.Id)
                    {
                        guideCommentsOverview.Add(MakeCommentList(comment.User.Id,guestsCheckPoint.CheckPoint,comment));
                        
                    }
                }
            }
            return guideCommentsOverview;
        }

        private GuideCommentsOverview MakeCommentList(int userId,CheckPoint checkPoint,Comment comment)
        {
            User user = _userService.GetUserById(userId);

            GuideCommentsOverview commentOverView = new GuideCommentsOverview(user, checkPoint, comment.Text, comment.IsReported);
            
            return commentOverView;

        }


    }
}
