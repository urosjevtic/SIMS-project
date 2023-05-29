using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IUnratedOwnerRepository
    {
        void Save(UnratedOwner unratedOwner);
        List<UnratedOwner> GetAll();
        void Remove(UnratedOwner unratedOwner);
    }
}
