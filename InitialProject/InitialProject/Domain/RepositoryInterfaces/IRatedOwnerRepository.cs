using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;


namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IRatedOwnerRepository
    {
        int NextId();

        void Save(RatedOwner ratedOwner);

        List<RatedOwner> GetAll();
    }
}

