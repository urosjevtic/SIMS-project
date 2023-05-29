using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface ISuperGuideStatusRepository
    {
        public int NextId();

        public void Save(SuperGuideStatus status);


        public List<SuperGuideStatus> GetAll();


        public SuperGuideStatus GetById(int id);

        public void Delete(SuperGuideStatus entity);


        public void SaveAll(List<SuperGuideStatus> entities);


        public void Update(SuperGuideStatus entity);


    }
}
