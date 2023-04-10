using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.Model.Reservations;

namespace InitialProject.Service
{
    public class RatedOwnerService
    {
        private readonly IRatedOwnerRepository _ratedOwnerRepository;
        public RatedOwnerService()
        {
            _ratedOwnerRepository = Injector.Injector.CreateInstance<IRatedOwnerRepository>();
        }

        public List<RatedOwner> GetAll()
        {
            return _ratedOwnerRepository.GetAll();
        }

        public RatedOwner Get(int id)
        {
            List<RatedOwner> ratedOwners = GetAll();
            return ratedOwners.FirstOrDefault(r => r.Id == id);
        }

        public void Save(RatedOwner owner)
        {
            _ratedOwnerRepository.Save(owner);
        }

    }
}