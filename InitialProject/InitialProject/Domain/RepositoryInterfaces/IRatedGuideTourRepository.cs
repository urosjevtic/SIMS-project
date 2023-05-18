using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IRatedGuideTourRepository
    {
        int NextId();
        void Save(RatedGuideTour ratedGuest);
        List<RatedGuideTour> GetAll();
    }
}
