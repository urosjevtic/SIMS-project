using InitialProject.Domain.Model.Users;
using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces.IUsersRepo;
using InitialProject.Serializer;

namespace InitialProject.Repository.UserRepo
{
    public class OwnerRepository : UserRepository, IOwnerRepository
    {
        private const string FilePath = "../../../Resources/Data/owners.csv";

        private readonly Serializer<Owner> _serializer;

        private List<Owner> _owners;

        public OwnerRepository()
        {
            _serializer = new Serializer<Owner>();
            _owners = _serializer.FromCSV(FilePath);
        }
        public List<Owner> GetAllOwners()
        {
            return _serializer.FromCSV(FilePath).Cast<Owner>().ToList();
        }
    }
}
