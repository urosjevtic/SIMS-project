using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IShortTourRequestRepository
    {
        public int NextId();
        public void Save(ShortTourRequest shortTourRequest);
        public List<ShortTourRequest> GetAll();
        public void Invalidate(ShortTourRequest shortTourRequest);
        public void Update(ShortTourRequest shortTourRequest);  
    }
}
