using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;

namespace InitialProject.Service
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        public UserService() 
        {
            _repository = Injector.Injector.CreateInstance<IUserRepository>();
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
