using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Service;
using InitialProject.Domain.Model;

namespace InitialProject.Service
{

    public class TourStatisticService
    {
        private ITourRepository _tourRepository;

        public TourService _tourService;
        public ITourReservationRepository _tourReservationRepository;
        public TourStatisticService()
        {
            _tourRepository = Injector.Injector.CreateInstance<ITourRepository>();
            _tourService = new TourService();
            _tourReservationRepository = Injector.Injector.CreateInstance<ITourReservationRepository>();
        }


        public int FindPeopleUntil18(Tour tour)
        {
            int guestsNumber = 0;
            foreach(TourReservation reservation in _tourReservationRepository.GetAll())
            {
                if(reservation.IdTour == tour.Id)
                {
                    if (reservation.AverageAge <= 18)
                    {
                        guestsNumber += reservation.NumberOfPeople;
                    }
                }
            }
            return guestsNumber;
        }
        public int FindPeopleBetween18and50(Tour tour)
        {
            int guestsNumber = 0;
            foreach (TourReservation reservation in _tourReservationRepository.GetAll())
            {
                if (reservation.IdTour == tour.Id)
                {
                    if (reservation.AverageAge > 18 && reservation.AverageAge<=50)
                    {
                        guestsNumber += reservation.NumberOfPeople;
                    }
                }
            }
            return guestsNumber;
        }
        public int FindPeopleOver50(Tour tour)
        {
            int guestsNumber = 0;
            foreach (TourReservation reservation in _tourReservationRepository.GetAll())
            {
                if (reservation.IdTour == tour.Id)
                {
                    if (reservation.AverageAge > 50)
                    {
                        guestsNumber += reservation.NumberOfPeople;
                    }
                }
            }
            return guestsNumber;
        }

        public double FindPeopleWithVoucher(Tour tour) 
        {

            double guestsNumber = 0;
            double withVoucher = 0;
            foreach (TourReservation reservation in _tourReservationRepository.GetAll())
            {
                if (reservation.IdTour == tour.Id)
                {
                    guestsNumber += reservation.NumberOfPeople;
                    if(reservation.IsUsingVoucher == true)
                    {
                        withVoucher+=reservation.NumberOfPeople;
                    }
                    
                }
            }
            if (guestsNumber == 0)
            {
                return 0;
            }
            else
            {
                return (withVoucher / guestsNumber) * 100;
            }
        }
        public double FindPeopleWithoutVoucher(Tour tour)
        {
            double guestsNumber = 0;
            double withoutVoucher = 0;
            foreach (TourReservation reservation in _tourReservationRepository.GetAll())
            {
                if (reservation.IdTour == tour.Id)
                {
                    guestsNumber += reservation.NumberOfPeople;
                    if (reservation.IsUsingVoucher == false)
                    {
                        withoutVoucher+=reservation.NumberOfPeople;
                    }

                }
            }
            if (guestsNumber == 0)
            {
                return 0;
            }
            else
            {
                return (withoutVoucher / guestsNumber) * 100;
            }
        }

    }
}
