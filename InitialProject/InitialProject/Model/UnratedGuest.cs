﻿using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class UnratedGuest : ISerializable
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Accommodation ReservedAccommodation { get; set; }
        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }

        public UnratedGuest() 
        {
            User = new User();
            ReservedAccommodation = new Accommodation();
        }

        public UnratedGuest(int id, User user,  Accommodation reservedAccommodation, DateTime reservationStartDate, DateTime reservationEndDate)
        {
            Id = id;
            User = user;
            ReservedAccommodation = reservedAccommodation;
            ReservationStartDate = reservationStartDate;
            ReservationEndDate = reservationEndDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {Id.ToString(),  User.Id.ToString(), ReservedAccommodation.Id.ToString(), ReservationStartDate.ToString("dd'/'MM'/'yyyy"), ReservationEndDate.ToString("dd'/'MM'/'yyyy") };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            User.Id = Convert.ToInt32(values[1]);
            ReservedAccommodation.Id = Convert.ToInt32(values[2]);
            ReservationStartDate = DateTime.ParseExact(values[3], "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);
            ReservationEndDate = DateTime.ParseExact(values[4], "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);

        }
    }
}
