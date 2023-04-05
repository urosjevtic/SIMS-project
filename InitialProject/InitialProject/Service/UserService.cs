using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class UserService
    {
        private readonly UserRepository _repository;
        public UserService() 
        {
            _repository = new UserRepository();
        }

        public List<User> GetAll()
        {
            return _repository.GetAll();
        }
        public User GetUserById(int userId)
        {
            return _repository.GetById(userId); 
        }
    }
}
