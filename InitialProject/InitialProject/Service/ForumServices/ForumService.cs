using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Forums;
using InitialProject.Domain.RepositoryInterfaces.IForumsRepo;

namespace InitialProject.Service.ForumServices
{
    public class ForumService
    {
        private readonly IForumRepository _forumRepository;
        private readonly AccommodationService _accommodationService;
        private readonly LocationService _locationService;
        private readonly UserService _userService;
        private readonly ForumCommentsService _forumCommentsService;

        public ForumService()
        {
            _forumRepository = Injector.Injector.CreateInstance<IForumRepository>();
            _forumCommentsService = new ForumCommentsService();
            _accommodationService = new AccommodationService();
            _userService = new UserService();
            _locationService = new LocationService();
        }


        public List<Forum> GetAll()
        {
            return _forumRepository.GetAll();
        }


        public List<Forum> GetByOwnerId(int ownerId)
        {
            List<Forum> forums = _forumRepository.GetAll();
            List<Accommodation> accommdoations = _accommodationService.GetAllAccommodationByOwnerId(ownerId);
            List<Forum> ownerForums = new List<Forum>();
            BindLocationsToForums(forums);
            BindAuthorToForum(forums);
            foreach (var accommodation in accommdoations)
            {
                foreach (var forum in forums)
                {
                    if (forum.Location.Equals(accommodation.Location))
                    {
                        ownerForums.Add(forum);
                    }
                }
            }


            return ownerForums;
        }

        private void BindLocationsToForums(List<Forum> forums)
        {
            List<Location> locations = _locationService.GetLocations();
            foreach (var forum in forums)
            {
                foreach (var location in locations)
                {
                    if (location.Id == forum.Location.Id)
                    {
                        forum.Location = location;
                    }
                }
            }
        }

        public void BindCommentsToForum(Forum forum)
        {
            List<ForumComment> forumComments = _forumCommentsService.GetAll();
            foreach (var comment in forum.Comments)
            {
                foreach (var forumComment in forumComments)
                {
                    if (comment.Id == forumComment.Id)
                    {
                        comment.Comment = forumComment.Comment;
                        comment.Author = forumComment.Author;
                        comment.NumberOfReports = forumComment.NumberOfReports;
                        comment.ReportedBy = forumComment.ReportedBy;
                        comment.HasUserReported = forumComment.HasUserReported;
                        break;
                    }
                }
            }

        }

        private void BindAuthorToForum(List<Forum> forums)
        {
            List<User> users = _userService.GetAll();
            foreach (var forum in forums)
            {
                foreach (var user in users)
                {
                    if (user.Id == forum.Author.Id)
                    {
                        forum.Author = user;
                    }
                }
            }
        }

        public ForumComment AddNewComment(Forum forum, User user, string comment)
        {
           ForumComment newComment = _forumCommentsService.Save(user, comment);
           forum.Comments.Add(newComment);
           _forumRepository.Update(forum);
           return newComment;
        }

        public void ReportComment(ForumComment comment, User reporter)
        {
            _forumCommentsService.Report(comment, reporter);
        }

        public void CheckWhatCommentsAreReported(User user, List<ForumComment> comments)
        {
            _forumCommentsService.HasUserReported(user, comments);
        }
    }
}
