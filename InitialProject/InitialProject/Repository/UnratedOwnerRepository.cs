using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System.Collections.Generic;
using InitialProject.Domain.Model;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InitialProject.Repository
{
    public class UnratedOwnerRepository : IUnratedOwnerRepository
    {
        private const string FilePath = "../../../Resources/Data/unratedOwners.csv";
        private readonly Serializer<UnratedOwner> _serializer;

        private List<UnratedOwner> _unratedOwners;

        public UnratedOwnerRepository()
        {
            _serializer = new Serializer<UnratedOwner>();
            _unratedOwners = _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _unratedOwners = _serializer.FromCSV(FilePath);
            if (_unratedOwners.Count < 1)
            {
                return 1;
            }
            return _unratedOwners.Max(c => c.Id) + 1;
        }

        public void Save(UnratedOwner unratedOwner)
        {
            unratedOwner.Id = NextId();
            _unratedOwners = _serializer.FromCSV(FilePath);
            _unratedOwners.Add(unratedOwner);
            _serializer.ToCSV(FilePath, _unratedOwners);
        }

        public List<UnratedOwner> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Remove(UnratedOwner unratedOwner)
        {
            foreach (UnratedOwner owner in _unratedOwners)
            {
                if (owner.Id == unratedOwner.Id)
                {
                    _unratedOwners.Remove(owner);
                    break;
                }
            }
            _serializer.ToCSV(FilePath, _unratedOwners);
        }
    }
}
