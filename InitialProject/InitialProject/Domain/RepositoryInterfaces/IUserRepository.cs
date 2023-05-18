using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        User GetByUsername(string username);
        User GetById(int id);
        List<User> GetAll();
        void SaveAll(List<User> users);
        void Update(User user);
    }
}
