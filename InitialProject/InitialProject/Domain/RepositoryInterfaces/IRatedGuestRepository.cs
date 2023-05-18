using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IRatedGuestRepository
    {
        int NextId();

        void Save(RatedGuest ratedGuest);

        List<RatedGuest> GetAll();
    }
}
