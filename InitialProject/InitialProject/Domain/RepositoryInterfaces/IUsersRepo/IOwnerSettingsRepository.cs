using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Users;

namespace InitialProject.Domain.RepositoryInterfaces.IUsersRepo
{
    public interface IOwnerSettingsRepository
    {
        List<OwnerSettings> GetAll();
        OwnerSettings GetByOwnerId(int ownerId);

        void Update(OwnerSettings ownerSettings);

    }
}
