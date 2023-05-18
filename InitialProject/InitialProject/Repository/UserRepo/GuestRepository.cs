using InitialProject.Domain.Model.Users;
using InitialProject.Domain.RepositoryInterfaces.IUsersRepo;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository.UserRepo
{
    public class GuestRepository :UserRepository, IGuestRepository
    {
        private const string FilePath = "../../../Resources/Data/guests.csv";

        private readonly Serializer<Guest> _serializer;

        private List<Guest> _guests;

        public GuestRepository()
        {
            _serializer = new Serializer<Guest>();
            _guests = _serializer.FromCSV(FilePath);
        }
        public List<Guest> GetAllGuests()
        {
            return _serializer.FromCSV(FilePath).Cast<Guest>().ToList();
        }
    }
}
