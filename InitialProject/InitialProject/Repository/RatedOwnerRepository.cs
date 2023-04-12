using InitialProject.Domain.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.Model.Reservations;

namespace InitialProject.Repository
{
    public class RatedOwnerRepository : IRatedOwnerRepository
    {
        private const string FilePath = "../../../Resources/Data/ratedOwners.csv";
        private readonly Serializer<RatedOwner> _serializer;

        private List<RatedOwner> _ratedOwners;

        public RatedOwnerRepository()
        {
            _serializer = new Serializer<RatedOwner>();
            _ratedOwners = _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _ratedOwners = _serializer.FromCSV(FilePath);
            if (_ratedOwners.Count < 1)
            {
                return 1;
            }
            return _ratedOwners.Max(c => c.Id) + 1;
        }



        public void Save(RatedOwner ratedOwner)
        {
            ratedOwner.Id = NextId();
            _ratedOwners = _serializer.FromCSV(FilePath);
            _ratedOwners.Add(ratedOwner);
            _serializer.ToCSV(FilePath, _ratedOwners);
        }


        public List<RatedOwner> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
    }
}

