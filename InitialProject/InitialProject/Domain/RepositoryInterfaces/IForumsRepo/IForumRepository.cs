using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Forums;

namespace InitialProject.Domain.RepositoryInterfaces.IForumsRepo
{
    public interface IForumRepository
    {
        List<Forum> GetAll();
        Forum GetByLocationId(int locationId);
        void Update(Forum forum);
        void Delete(Forum forum);
    }
}
