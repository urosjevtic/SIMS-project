using InitialProject.Domain.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces.IUsersRepo
{
    public interface IGuestRepository
    {
        List<Guest> GetAllGuests();
    }
}
