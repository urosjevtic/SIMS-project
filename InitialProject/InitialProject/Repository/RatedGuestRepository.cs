using InitialProject.Domain.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;

namespace InitialProject.Repository
{
    public class RatedGuestRepository : IRatedGuestRepository
    {
        private const string FilePath = "../../../Resources/Data/ratedGuests.csv";
        private readonly Serializer<RatedGuest> _serializer;

        private List<RatedGuest> _ratedGuests;

        public RatedGuestRepository()
        {
            _serializer = new Serializer<RatedGuest>();
            _ratedGuests = _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _ratedGuests = _serializer.FromCSV(FilePath);
            if (_ratedGuests.Count < 1)
            {
                return 1;
            }
            return _ratedGuests.Max(c => c.Id) + 1;
        }


        public void Save(RatedGuest ratedGuest)
        {
            ratedGuest.Id = NextId();
            _ratedGuests = _serializer.FromCSV(FilePath);
            _ratedGuests.Add(ratedGuest);
            _serializer.ToCSV(FilePath, _ratedGuests);
        }


        public List<RatedGuest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
    }
}
