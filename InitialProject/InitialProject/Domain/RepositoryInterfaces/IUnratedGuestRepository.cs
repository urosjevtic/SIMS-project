using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IUnratedGuestRepository
    {
        void Save(UnratedGuest unratedGuest);
        List<UnratedGuest> GetAll();
        void Remove(UnratedGuest unratedGuest);

    }
}
