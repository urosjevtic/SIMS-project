using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;

namespace InitialProject.Repository
{
    public class RatedGuideTourRepository : IRatedGuideTourRepository
    {
        private const string FilePath = "../../../Resources/Data/ratedGuideTours.csv";

        private readonly Serializer<RatedGuideTour> _serializer;
        public List<RatedGuideTour> ratedGuideTours { get; set; }

        public RatedGuideTourRepository()
        {
            _serializer = new Serializer<RatedGuideTour>();
            ratedGuideTours = _serializer.FromCSV(FilePath);
        }
        public List<RatedGuideTour> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            ratedGuideTours = _serializer.FromCSV(FilePath);
            if (ratedGuideTours.Count < 1)
            {
                return 1;
            }
            return ratedGuideTours.Max(c => c.Id) + 1;
        }

        public void Save(RatedGuideTour ratedGuideTour)
        {
            ratedGuideTour.Id = NextId();
            ratedGuideTours = _serializer.FromCSV(FilePath);
            ratedGuideTours.Add(ratedGuideTour);
            _serializer.ToCSV(FilePath, ratedGuideTours);
        }

    }
}
