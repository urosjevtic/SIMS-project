using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Serializer;
using InitialProject.Domain.RepositoryInterfaces;

namespace InitialProject.Repository
{
    public class SuperGuideStatusRepository : ISuperGuideStatusRepository
    {

        private const string FilePath = "../../../Resources/Data/superGuide.csv";
        private readonly Serializer<SuperGuideStatus> _serializer;

        private List<SuperGuideStatus> _superGuides;

        public SuperGuideStatusRepository()
        {
            _serializer = new Serializer<SuperGuideStatus>();
            _superGuides = _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _superGuides = _serializer.FromCSV(FilePath);
            if (_superGuides.Count < 1)
            {
                return 1;
            }
            return _superGuides.Max(c => c.Id) + 1;
        }

        public void Save(SuperGuideStatus guest)
        {
            guest.Id = NextId();
            _superGuides = _serializer.FromCSV(FilePath);
            _superGuides.Add(guest);
            _serializer.ToCSV(FilePath, _superGuides);
        }

        public List<SuperGuideStatus> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public SuperGuideStatus GetById(int id)
        {
            _superGuides = _serializer.FromCSV(FilePath);
            return _superGuides.FirstOrDefault(g => g.Id == id);
        }

        public void Delete(SuperGuideStatus entity)
        {
            _superGuides = _serializer.FromCSV(FilePath);
            SuperGuideStatus founded = _superGuides.Find(c => c.Id == entity.Id);
            _superGuides.Remove(founded);
            _serializer.ToCSV(FilePath, _superGuides);
        }

        public void SaveAll(List<SuperGuideStatus> entities)
        {
            _serializer.ToCSV(FilePath, _superGuides);
        }

        public void Update(SuperGuideStatus entity)
        {
            SuperGuideStatus newGuest = _superGuides.Find(p => p.Id == entity.Id);
            newGuest.Id = entity.Id;
            newGuest.Language = entity.Language;
            newGuest.AverageRating = entity.AverageRating;
            newGuest.ToursNumber = entity.ToursNumber;
           
            SaveAll(_superGuides);
        }

       
    }
}
