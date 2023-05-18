using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace InitialProject.Domain.RepositoryInterfaces.IAccommodationRepo
{
    public interface IAccommodationRepository
    {
        Accommodation Save(Accommodation accommodation);
        List<Accommodation> GetAll();

        Accommodation GetById(int accommodationId);

        void Update(Accommodation accommodation);
    }
}
