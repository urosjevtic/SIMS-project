using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Forums;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.RepositoryInterfaces.IForumsRepo;
using InitialProject.Service.ReservationServices;

namespace InitialProject.Service.ForumServices
{
    public class ForumCommentsService
    {
        private readonly IForumCommentRepository _forumCommentRepository;
        private readonly UserService _userService;
        private readonly AccommodationService _accommodationService;
        private readonly AccommodationReservationService _accommodationReservationService;

        public ForumCommentsService()
        {
            _forumCommentRepository = Injector.Injector.CreateInstance<IForumCommentRepository>();
            _userService = new UserService();
            _accommodationService = new AccommodationService();
            _accommodationReservationService = new AccommodationReservationService();
        }


        public List<ForumComment> GetAll()
        {
            List<ForumComment> comments = _forumCommentRepository.GetAll();
            BindUserToComment(comments);
            return comments;
        }

        private void BindUserToComment(List<ForumComment> forumComments)
        {
            List<User> users = _userService.GetAll();
            foreach (var comment in forumComments)
            {
                comment.Author = users.FirstOrDefault(user => user.Id == comment.Author.Id);
            }
        }

        public ForumComment Save(User user, string commentText)
        {
            ForumComment comment = new ForumComment();
            comment.Author = user;
            comment.Comment = commentText;
            comment.NumberOfReports = 0;
            return _forumCommentRepository.Save(comment);
        }

        public void Report(ForumComment comment, User reporter)
        {
            comment.NumberOfReports++;
            comment.ReportedBy.Add(reporter);
            comment.HasUserReported = true;
            _forumCommentRepository.Update(comment);
        }

        public void RemoveReport(ForumComment comment, User reporter)
        {
            comment.NumberOfReports--;
            foreach (var user in comment.ReportedBy)
            {
                if (user.Id == reporter.Id)
                {
                    comment.ReportedBy.Remove(user);
                    break;
                }
            }
            comment.HasUserReported = false;
            _forumCommentRepository.Update(comment);
        }

        public void HasUserReported(User user, List<ForumComment> comments)
        {
            foreach (var comment in comments)
            {
                foreach (var reporter in comment.ReportedBy)
                {
                    if (reporter.Id == user.Id)
                    {
                        comment.HasUserReported = true;
                        break;
                    }
                }
            }
        }

        public void IsByOwnerWithAccommodationOnLocation(Forum forum, ForumComment comment)
        {
            List<Accommodation> accommodations = _accommodationService.GetAllAccommodationByOwnerId(comment.Author.Id);
            foreach (var accommodation in accommodations)
            {
                if (accommodation.Location.Equals(forum.Location))
                {
                    comment.IsByOwnerThatHasAccommdoationOnThisLocation = true;
                    break;
                }
            }

        }

        public void IsByGuestThatVisitedLocation(Forum forum, ForumComment comment)
        {
            List<AccommodationReservation> accommodationReservations =
                _accommodationReservationService.GetAllReservationByGuestId(comment.Author.Id);
            foreach (var reservation in accommodationReservations)
            {
                if (reservation.Accommodation.Location.Equals(forum.Location))
                {
                    comment.IsByGuestThatVisited = true;
                    break;
                }
            }
        }

    }
}
