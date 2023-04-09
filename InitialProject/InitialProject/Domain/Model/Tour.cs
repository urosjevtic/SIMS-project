﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;
using InitialProject.Repository;

namespace InitialProject.Domain.Model
{

    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public User Guide { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public List<CheckPoint> CheckPoints { get; set; }
        public int MaxGuests { get; set; }
        public DateTime Start { get; set; }
        public int Duration { get; set; }
        public Image CoverImageUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsRated { get; set; }

        public CheckPointRepository _checkPointRepository { get; set; }
        public Tour()
        {
            Location = new Location();
            Guide = new User();
            CheckPoints = new List<CheckPoint>();
            CoverImageUrl = new Image();
            _checkPointRepository = new CheckPointRepository();
        }
        public Tour(int id, User giude, string name, Location location, string description, string language, List<CheckPoint> checkPoints, int maxGuests, DateTime start, int duration, Image coverImageUrl, bool isActve, bool rated)
        {
            Id = id;
            Guide = giude;
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            CheckPoints = checkPoints;
            MaxGuests = maxGuests;
            Start = start;
            Duration = duration;
            CoverImageUrl = coverImageUrl;
            IsActive = isActve;
            IsRated = rated;
        }
        public string Concatenate()
        {
            return Location.Country + " " + Location.City + " " + Language;
        }
        public string[] ToCSV()
        {
            string[] csvValues = {
               Id.ToString(),
               Guide.Id.ToString(),
               Name,
               Location.Id.ToString(),
               Description,
               Language,
               MaxGuests.ToString(),
               Start.ToString(),
               Duration.ToString(),
               CoverImageUrl.Id.ToString(),
               IsActive.ToString(),
               IsRated.ToString(),
            };

            if (CheckPoints.Count() > 0)
            {
                foreach (CheckPoint checkPoint in CheckPoints)
                {
                    Array.Resize(ref csvValues, csvValues.Length + 1);
                    csvValues[csvValues.Length - 1] = checkPoint.Id.ToString();
                }
            }

            Array.Resize(ref csvValues, csvValues.Length + 1);
            csvValues[csvValues.Length - 1] = "[END]";

            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Guide.Id = Convert.ToInt32(values[1]);
            Name = values[2];
            Location.Id = Convert.ToInt32(values[3]);
            Description = values[4];
            Language = values[5];
            MaxGuests = Convert.ToInt32(values[6]);
            Start = DateTime.Parse(values[7]);
            Duration = Convert.ToInt32(values[8]);
            CoverImageUrl.Id = Convert.ToInt32(values[9]);
            IsActive = Convert.ToBoolean(values[10]);
            IsRated = Convert.ToBoolean(values[11]);

            int i = 12;
            CheckPoints.Clear();



            while (values[i] != "[END]")
            {
                int ids = Convert.ToInt32(values[i]);
                CheckPoints.Add(_checkPointRepository.GetById(ids));
                i++;
            }
        }





    }
}
