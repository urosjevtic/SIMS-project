using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IGuestsCheckPointRepository
    {
        public List<GuestsCheckPoint> GetAll();

        public GuestsCheckPoint GetById(int id);


        public void Save(GuestsCheckPoint checkPoint);

        public int NextId();
    }
}
