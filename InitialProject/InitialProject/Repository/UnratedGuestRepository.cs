﻿using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class UnratedGuestRepository
    {
        private const string FilePath = "../../../Resources/Data/unratedGuests.csv";
        private readonly Serializer<UnratedGuest> _serializer;

        private List<UnratedGuest> _unratedGuests;

        public UnratedGuestRepository()
        {
            _serializer = new Serializer<UnratedGuest>();
            _unratedGuests = _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _unratedGuests = _serializer.FromCSV(FilePath);
            if (_unratedGuests.Count < 1)
            {
                return 1;
            }
            return _unratedGuests.Max(c => c.Id) + 1;
        }

        public void Save(UnratedGuest unratedGuest)
        {
            unratedGuest.Id = NextId();
            _unratedGuests = _serializer.FromCSV(FilePath);
            _unratedGuests.Add(unratedGuest);
            _serializer.ToCSV(FilePath, _unratedGuests);
        }

        public List<UnratedGuest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
    }
}