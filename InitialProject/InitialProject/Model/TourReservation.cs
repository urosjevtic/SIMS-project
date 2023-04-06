﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Model
{
    public class TourReservation : ISerializable
    {
        public int IdReservation { get; set; }
        public int IdTour { get; set; }
        public int IdGuest { get; set; }
        public int NumberOfPeople { get; set; }

       

        public TourReservation() { }
        

        public string[] ToCSV()
        {
            string[] csvValues = {
               IdReservation.ToString(),
               IdTour.ToString(),
               IdGuest.ToString(),
               NumberOfPeople.ToString()
            };

            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            IdReservation = Convert.ToInt32(values[0]);
            IdTour = Convert.ToInt32(values[1]);
            IdGuest = Convert.ToInt32(values[2]);
            NumberOfPeople = Convert.ToInt32(values[3]);
        }
    }
}