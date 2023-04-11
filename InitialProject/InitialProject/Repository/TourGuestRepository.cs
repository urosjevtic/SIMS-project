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
    public class TourGuestRepository : ITourGuestRepository
    {

        private const string FilePath = "../../../Resources/Data/tourGuests.csv";
        private readonly Serializer<TourGuest> _serializer;

        private readonly UserRepository _userRepository;    
        private List<TourGuest> _guests;

        public TourGuestRepository()
        {
            _serializer = new Serializer<TourGuest>();
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

        public void Save(TourGuest guest)
        {
            guest.Id = NextId();
            _guests = _serializer.FromCSV(FilePath);
            _guests.Add(guest);
            _serializer.ToCSV(FilePath, _guests);
        }

        public List<TourGuest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourGuest GetById(int id)
        {
            _guests = _serializer.FromCSV(FilePath);
            return _guests.FirstOrDefault(g => g.Id == id);
        }

        public void Delete(TourGuest entity)
        {
            _guests = _serializer.FromCSV(FilePath);
            TourGuest founded = _guests.Find(c => c.Id == entity.Id);
            _guests.Remove(founded);
            _serializer.ToCSV(FilePath, _guests);
        }

        public void SaveAll(List<TourGuest> entities)
        {
            _serializer.ToCSV(FilePath, _guests);
        }

        public void Update(TourGuest entity)
        {
            TourGuest newGuest = _guests.Find(p=> p.Id == entity.Id);
            newGuest.Id = entity.Id;
            newGuest.Username = entity.Username;
            newGuest.Role = entity.Role;
            newGuest.Password = entity.Password;
            newGuest.Name = entity.Name;
            newGuest.Surname = entity.Surname;
            newGuest.Presence = entity.Presence;
            newGuest.CheckPointName = entity.CheckPointName;
            SaveAll(_guests);
        }
       
        public TourGuest GetGuest(User user)
        {
            foreach(User u in _userRepository.GetAll())
            {
                foreach(TourGuest guest in GetAll())
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
