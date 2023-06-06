using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IComplexTourRequestRepository
    {
        public int NextId();
        public List<ShortTourRequest> NextIdForParts(List<ShortTourRequest> list);
        public void Save(ComplexTourRequest complexTourRequest);
        public List<ComplexTourRequest> GetAll();
    }
}
