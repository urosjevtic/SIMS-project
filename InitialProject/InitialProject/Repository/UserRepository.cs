using InitialProject.Domain.Model;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;
using InitialProject.Domain.RepositoryInterfaces;

namespace InitialProject.Repository
{
    public class UserRepository : IUserRepository
    {
        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> _serializer;

        private List<User> _users;

        public UserRepository()
        {
            _serializer = new Serializer<User>();
            _users = _serializer.FromCSV(FilePath);
        }

        public User GetByUsername(string username)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.FirstOrDefault(u => u.Username == username);
        }
        public User GetById(int id)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.FirstOrDefault(u => u.Id == id);
        }
        public List<User> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void SaveAll(List<User> _users)
        {
            _serializer.ToCSV(FilePath, _users);
        }
        public void Update(User user)
        {
            User newUser = _users.Find(p1 => p1.Id == user.Id);
            newUser.Id = user.Id;
            newUser.Username = user.Username;
            newUser.Password = user.Password;
            newUser.Role = user.Role;
            
            SaveAll(_users);

        }
    }
}
