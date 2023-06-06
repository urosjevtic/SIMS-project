using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces.IAccommodationRepo;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Statistics;
using InitialProject.Service.StatisticService;

namespace InitialProject.Service
{


    public class AccommodationService
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly LocationService _locationService;
        private readonly ImageService _imageService;
        private readonly UserService _userService;
        private readonly AccommodationStatisticService _accommodationStatisticService;

        public AccommodationService()
        {
            _accommodationRepository = Injector.Injector.CreateInstance<IAccommodationRepository>();
            _locationService = new LocationService();
            _imageService = new ImageService();
            _userService = new UserService();
            _accommodationStatisticService = new AccommodationStatisticService();
        }

        public void CreateAccommodation(string name, string country, string city, string type, int maxGuests, int minReservationDays, int cancelationPeriod, List<string> imagesUrl, int ownerId)
        {
            Accommodation accommodation = new Accommodation();
            CreateNewAccommodation(accommodation, name, country, city, type, maxGuests, minReservationDays, cancelationPeriod, imagesUrl, ownerId);
            int accommodationId = _accommodationRepository.Save(accommodation).Id;
            _accommodationStatisticService.CreateStatisticForNewAccommodation(accommodationId);

        }



        private void CreateNewAccommodation(Accommodation accommodation, string name, string country, string city, string type, int maxGuests, int minReservationDays, int cancelationPeriod, List<string> imagesUrl, int ownerId)
        {
            accommodation.Owner.Id = ownerId;
            accommodation.Name = name;
            accommodation.Location.Id = _locationService.GetLocationId(country, city);
            accommodation.Type = GetType(type);
            accommodation.MaxGuests = maxGuests;
            accommodation.MinReservationDays = minReservationDays;
            accommodation.CancelationPeriod = cancelationPeriod;
            _imageService.SaveImages(imagesUrl);
        }

        public List<Accommodation> GetAll()
        {
            List <Accommodation> accommodations = _accommodationRepository.GetAll();
            BindUserToAccommodation(accommodations);
            return accommodations;
        }

        private void BindUserToAccommodation(List<Accommodation> accommodations)
        {
            foreach (var accommodation in accommodations)
            {
                foreach (var user in _userService.GetAll())
                {
                    if (accommodation.Owner.Id == user.Id)
                    {
                        accommodation.Owner = user;
                        break;
                    }

                }
            }
        }
        private AccommodationType GetType(string type)
        {
            switch (type)
            {
                case "house":
                    return AccommodationType.house;
                case "cabin":
                    return AccommodationType.cabin;
                case "appartment":
                    return AccommodationType.appartment;
                default:
                    throw new Exception("Error has occurred");
            }
        }

        public List<Accommodation> GetAllAccommodationByOwnerId(int ownerId)
        {
            List<Accommodation> allAccommodation = GetAll();
            List<Accommodation> accommodations = new List<Accommodation>();
            foreach(Accommodation accommodation in allAccommodation)
            {
                if (accommodation.Owner.Id == ownerId)
                {
                    BindLocationToAccommodation(accommodation);
                    accommodations.Add(accommodation);
                }
            }

            return accommodations;
        }

        private void BindLocationToAccommodation(Accommodation accommodation)
        {
            foreach (var location in _locationService.GetLocations())
            {
                if (accommodation.Location.Id == location.Id)
                {
                    accommodation.Location = location;
                }
            }
        }

        public Accommodation GetById(int accommodationId)
        {
          Accommodation accommodation =  _accommodationRepository.GetById(accommodationId);
          BindLocationToAccommodation(accommodation);
          return accommodation;
        }

        public void ChangeRecentlyRenovatedStatus(Accommodation accommodation)
        {
            accommodation.IsRecentlyRenovated = !accommodation.IsRecentlyRenovated;
            _accommodationRepository.Update(accommodation);
        }

    }
}
