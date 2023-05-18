using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using InitialProject.Domain.RepositoryInterfaces.IAccommodationRepo;
using InitialProject.Domain.Model;
using Microsoft.VisualBasic.ApplicationServices;

namespace InitialProject.Repository.AccommodationRepo
{
    public class AccommodationRepository : IAccommodationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodation.csv";

        private readonly Serializer<Accommodation> _serializer;
        private readonly LocationRepository _locationRepository;

        private List<Accommodation> _accommodations;

        public AccommodationRepository()
        {
            _locationRepository = new LocationRepository();
            _serializer = new Serializer<Accommodation>();
            _accommodations = _serializer.FromCSV(FilePath);
        }

        private int NextId()
        {
            _accommodations = _serializer.FromCSV(FilePath);
            if (_accommodations.Count < 1)
            {
                return 1;
            }
            return _accommodations.Max(c => c.Id) + 1;
        }
        public Accommodation Save(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            _accommodations = _serializer.FromCSV(FilePath);
            _accommodations.Add(accommodation);
            _serializer.ToCSV(FilePath, _accommodations);

            return accommodation;
        }

        public List<Accommodation> GetAll()
        {
            List<Accommodation> accommodations = _serializer.FromCSV(FilePath);

            foreach (Accommodation accommodation in accommodations)
            {
                accommodation.Location = _locationRepository.GetById(accommodation.Location.Id);
            }

            return accommodations;
        }

        public Accommodation GetById(int accommodationId)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            return _accommodations.FirstOrDefault(a => a.Id == accommodationId);
        }

        public void Update(Accommodation newAccommodation)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            foreach (var accommodation in _accommodations)
            {
                if (accommodation.Id == newAccommodation.Id)
                {
                    _accommodations.Remove(accommodation);
                    _accommodations.Add(newAccommodation);
                    break;
                }
            }

            _serializer.ToCSV(FilePath, _accommodations);
        }
    }
}
