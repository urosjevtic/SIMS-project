using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.AccommodationRenovation;
using InitialProject.Domain.RepositoryInterfaces.IAccommodationRenovationsRepo;
using InitialProject.Serializer;

namespace InitialProject.Repository.AccommodationRenovationRepo
{
    public class RenovationRepository : IRenovationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationRenovations.csv";

        private readonly Serializer<Renovation> _serializer;

        private List<Renovation> _renovations;

        public RenovationRepository()
        {
            _serializer = new Serializer<Renovation>();
            _renovations = _serializer.FromCSV(FilePath);
        }

        private int NextId()
        {
            _renovations = _serializer.FromCSV(FilePath);
            if (_renovations.Count < 1)
            {
                return 1;
            }
            return _renovations.Max(c => c.Id) + 1;
        }


        public void Delete(Renovation selectedRenovation)
        {
            _renovations = _serializer.FromCSV(FilePath);
            foreach (var renovation in _renovations)
            {
                if (renovation.Id == selectedRenovation.Id)
                {
                    _renovations.Remove(renovation);
                    break;
                }
            }
            _serializer.ToCSV(FilePath, _renovations);
        }

        public void Save(Renovation renovation)
        {
            renovation.Id = NextId();
            _renovations = _serializer.FromCSV(FilePath);
            _renovations.Add(renovation);
            _serializer.ToCSV(FilePath, _renovations);
        }

        public List<Renovation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<Renovation> GetByAccommodationId(int accommodationId)
        {
            _renovations = _serializer.FromCSV(FilePath);
            return _renovations.FindAll(r => r.Accommodation.Id == accommodationId);
        }

    }
}
