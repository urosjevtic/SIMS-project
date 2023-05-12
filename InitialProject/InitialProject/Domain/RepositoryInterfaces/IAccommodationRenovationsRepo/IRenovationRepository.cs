using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.AccommodationRenovation;

namespace InitialProject.Domain.RepositoryInterfaces.IAccommodationRenovationsRepo
{
    public interface IRenovationRepository
    {
        List<Renovation> GetAll();
        List<Renovation> GetByAccommodationId(int id);

        void Delete(Renovation renovation);
        void Save(Renovation renovation);
    }
}
