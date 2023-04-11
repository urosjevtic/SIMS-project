﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.Model;

namespace InitialProject.Service
{
    public class GuestsCheckPointService
    {
        private IGuestsCheckPointRepository _guestCheckPointrepository;
        public GuestsCheckPointService()
        {
            _guestCheckPointrepository = Injector.Injector.CreateInstance<IGuestsCheckPointRepository>();
        }

        public List<GuestsCheckPoint> GetAll()
        {
            return _guestCheckPointrepository.GetAll();
        }

        public GuestsCheckPoint GetById(int id)
        {
           return _guestCheckPointrepository.GetById(id);
        }

        public void Save(GuestsCheckPoint guestCheckPoint)
        {
            _guestCheckPointrepository.Save(guestCheckPoint);
        }

        public int NextId()
        {
            return _guestCheckPointrepository.NextId();
        }
    }
}
