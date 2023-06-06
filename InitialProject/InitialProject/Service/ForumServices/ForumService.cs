using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Forums;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.RepositoryInterfaces.IForumsRepo;
using InitialProject.Repository.ForumsRepo;
using InitialProject.Service.ReservationServices;
using InitialProject.Service.StatisticService;
using InitialProject.ViewModels.ForumsViewModel;

namespace InitialProject.Service.ForumServices
{
    public class ForumService
    {
        private readonly IForumRepository _forumRepository;
        private readonly AccommodationService _accommodationService;
        private readonly LocationService _locationService;
        private readonly UserService _userService;
        private readonly ForumCommentsService _forumCommentsService;
        private readonly ForumCommentRepository _forumCommentRepository;
        private readonly AccommodationReservationService _accommodationReservationService;

        public User LoggedUser = App.LoggedUser;

        public ForumService()
        {
            _forumRepository = Injector.Injector.CreateInstance<IForumRepository>();
            _forumCommentsService = new ForumCommentsService();
            _forumCommentRepository= new ForumCommentRepository();
            _accommodationService = new AccommodationService();
            _userService = new UserService();
            _locationService = new LocationService();
            _accommodationReservationService = new AccommodationReservationService();
        }


        public List<Forum> GetAll()
        {
            List<Forum> allForums = _forumRepository.GetAll();
            BindLocationsToForums(allForums);
            BindAuthorToForum(allForums);
            return allForums;
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


        public List<Forum> GetByCity(string city)
        {
            List<Forum> forums = _forumRepository.GetAll();
            // List<Accommodation> accommdoations = _accommodationService.GetAllAccommodationByOwnerId(ownerId);
            List<Accommodation> accommodations = _accommodationService.GetAll();
            List<Forum> ownerForums = new List<Forum>();
            BindLocationsToForums(forums);
           // BindAuthorToForum(forums);
           foreach(var forum in forums)
            {
                if (forum.Location.Equals(city))
                {
                    ownerForums.Add(forum);
                }
            }
            //foreach (var accommodation in accommodations)
            //{

            //    //foreach (var forum in forums)
            //    //{
            //    //    if (forum.Location.Equals(accommodation.Location))
            //    //    {
            //    //        ownerForums.Add(forum);
            //    //    }
            //    //}
            //}
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

        public Forum Save( User author, string country, string city,DateTime date, bool isOpen, string comment)
        {
            ForumComment forumComment = new ForumComment();
            forumComment.Comment = comment;

            Forum forum = new Forum();
            forum.Author = author;
           forum.Location.Id = _locationService.GetLocationId(country, city);
           //forum.Location.City = city;
            //forum.Location.Country = country;
            forum.CreationDate = date;
            forum.IsOpen = isOpen;
            AddNewComment(forum, author, comment);
            //forum.Comments.Add(forumComment);
            return _forumRepository.Save(forum);

        }


        //public void CreateAccommodation(string name, string country, string city, string type, int maxGuests, int minReservationDays, int cancelationPeriod, string imagesUrl, int ownerId)
        //{
        //    Accommodation accommodation = new Accommodation();
        //    CreateNewAccommodation(accommodation, name, country, city, type, maxGuests, minReservationDays, cancelationPeriod, imagesUrl, ownerId);
        //   // int accommodationId = _accommodationRepository.Save(accommodation).Id;
        //   // _accommodationStatisticService.CreateStatisticForNewAccommodation(accommodationId);

        //}

        public void CreateForum(User author, string country, string city, DateTime date, bool isOpen, string comment)
        {
            Forum forum = new Forum();
            Create(forum, author, country, city, date, isOpen, comment);
            int forumId = _forumRepository.Save(forum).Id;
        }


        //private void CreateNewAccommodation(Accommodation accommodation, string name, string country, string city, string type, int maxGuests, int minReservationDays, int cancelationPeriod, string imagesUrl, int ownerId)
        //{
        //    accommodation.Owner.Id = ownerId;
        //    accommodation.Name = name;
        //    accommodation.Location.Id = _locationService.GetLocationId(country, city);
        //    accommodation.Type = GetType(type);
        //    accommodation.MaxGuests = maxGuests;
        //    accommodation.MinReservationDays = minReservationDays;
        //    accommodation.CancelationPeriod = cancelationPeriod;
        //    _imageService.SaveImages(0, imagesUrl);
        //}

        public void Create(Forum forum, User author, string country, string city, DateTime date, bool isOpen, string comment)
        {
            ForumComment forumComment = new ForumComment();
            forumComment.Comment = comment;
            AddNewComment(forum, author, comment);
            forum.Author = author;
            forum.Location.Id = _locationService.GetLocationId(country, city);
            forum.CreationDate = date;
            forum.IsOpen = isOpen;
            forum.Comments.Add(forumComment);
           // return _forumRepository.Save(forum);
        }





        public void ChangeStatus(Forum forum, bool isOpen)
        {
            forum.IsOpen=isOpen;
            _forumRepository.Update(forum);
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

        public void RemoveCommentReport(ForumComment comment, User reporter)
        {
            _forumCommentsService.RemoveReport(comment, reporter);
        }

        public void CheckWhatCommentsAreReported(User user, List<ForumComment> comments)
        {
            _forumCommentsService.HasUserReported(user, comments);
        }

        public bool IsSpecial(Forum forum)
        {
            int ownerCommentCount = 0;
            int visitingGuestCommentCount = 0;
            BindCommentsToForum(forum);
            List<ForumComment> comments = forum.Comments;
            foreach (var comment in comments)
            {
                if(comment.Author.Role == UserRole.Owner)
                    ownerCommentCount++;
                if (comment.Author.Role == UserRole.Guest)
                {
                    if (HasVisitedLocation(comment.Author, forum.Location))
                        visitingGuestCommentCount++;
                }
            }

            return ownerCommentCount > 1 && visitingGuestCommentCount > 2;
        }

        private bool HasVisitedLocation(User user, Location location)
        {
            List<AccommodationReservation> guestReservations = _accommodationReservationService.GetAllReservationByGuestId(user.Id);
            foreach (var guestReservation in guestReservations)
            {
                if (guestReservation.Accommodation.Location.Equals(location))
                    return true;
            }

            return false;
        }

        public void CheckUserRole(Forum forum, ForumComment comment)
        {
            if(comment.Author.Role.Equals(UserRole.Owner)) 
                _forumCommentsService.IsByOwnerWithAccommodationOnLocation(forum, comment);
            if (comment.Author.Role.Equals(UserRole.Guest))
                _forumCommentsService.IsByGuestThatVisitedLocation(forum, comment);
        }

        public bool CanOwnerMakeComment(Forum forum, int ownerId)
        {
            List<Accommodation> accommodations = _accommodationService.GetAllAccommodationByOwnerId(ownerId);

            foreach (var accommdoation in accommodations)
            {
                if (accommdoation.Location.Equals(forum.Location))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
