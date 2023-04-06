using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IRescheduleRequestRepository
    {
        RescheduleRequest Save(RescheduleRequest rescheduleRequest);

        List<RescheduleRequest> GetAll();

        void Delete(RescheduleRequest request);
    }
}
