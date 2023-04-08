using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Model;
using InitialProject.Serializer;
using InitialProject.Domain.RepositoryInterfaces;

namespace InitialProject.Repository
{
    public class TourGuestsRepository : ITourGuestRepository
    {

        private const string FilePath = "../../../Resources/Data/tourGuests.csv";
        private readonly Serializer<Guest> _serializer;

        private readonly UserRepository _userRepository;    
        private List<Guest> _guests;

        public TourGuestsRepository()
        {
            _serializer = new Serializer<Guest>();
            _guests = _serializer.FromCSV(FilePath);
            _userRepository = new UserRepository(); 
        }

        public int NextId()
        {
            _guests = _serializer.FromCSV(FilePath);
            if (_guests.Count < 1)
            {
                return 1;
            }
            return _guests.Max(c => c.Id) + 1;
        }

        public void Save(Guest guest)
        {
            guest.Id = NextId();
            _guests = _serializer.FromCSV(FilePath);
            _guests.Add(guest);
            _serializer.ToCSV(FilePath, _guests);
        }

        public List<Guest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Guest GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guest entity)
        {
            _guests = _serializer.FromCSV(FilePath);
            Guest founded = _guests.Find(c => c.Id == entity.Id);
            _guests.Remove(founded);
            _serializer.ToCSV(FilePath, _guests);
        }

        public void SaveAll(List<Guest> entities)
        {
            _serializer.ToCSV(FilePath, _guests);
        }

        public void Update(Guest entity)
        {
            Guest newGuest = _guests.Find(p=> p.Id == entity.Id);
            newGuest.Id = entity.Id;
            newGuest.Username = entity.Username;
            newGuest.Role = entity.Role;
            newGuest.Password = entity.Password;
            newGuest.Name = entity.Name;
            newGuest.Surname = entity.Surname;
            newGuest.Presence = entity.Presence;

            SaveAll(_guests);
        }
       
        public Guest GetGuest(User user)
        {
            foreach(User u in _userRepository.GetAll())
            {
                foreach(Guest guest in GetAll())
                {
                    if(guest.Id == u.Id)
                    {
                        return guest;
                    }
                }
            }
            return null;
        }


    }
}
