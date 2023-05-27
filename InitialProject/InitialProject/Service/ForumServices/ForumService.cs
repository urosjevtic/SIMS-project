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
                        break;
                    }
                }
            }

        }
    }
}
