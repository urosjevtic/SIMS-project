using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Service;
using InitialProject.Domain.Model;
using System.Text.RegularExpressions;

namespace InitialProject.Service
{
    public class TourStatisticService
    {
        public TourService _tourService;
        public ITourReservationRepository _tourReservationRepository;
        public IShortTourRequestRepository _shortTourRequestRepository;
        public TourStatisticService()
        {
            _tourService = new TourService();
            _tourReservationRepository = Injector.Injector.CreateInstance<ITourReservationRepository>();
            _shortTourRequestRepository = Injector.Injector.CreateInstance<IShortTourRequestRepository>();
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
        public List<DataPoint> FindToursByLanguage(int year)
        {
            List<DataPoint> list = new ();
            AddLanguages(list);
            int i;
            foreach (DataPoint dp in list)
            {
                i = 0;
                foreach(ShortTourRequest shortRequest in _shortTourRequestRepository.GetAll())
                {
                    if((dp.Key == shortRequest.Language) && Convert.ToInt32(year) == shortRequest.From.Year && shortRequest.Status == RequestStatus.Accepted)
                    {
                        i++;
                    }
                }
                dp.Value = i;
            }
            return list;
        }
        public List<DataPoint> FindToursByLanguageAllTimes()
        {
            List<DataPoint> list = new();
            AddLanguages(list);
            int i;
            foreach (DataPoint dp in list)
            {
                i = 0;
                foreach (ShortTourRequest shortRequest in _shortTourRequestRepository.GetAll())
                {
                    if ((dp.Key == shortRequest.Language) && shortRequest.Status == RequestStatus.Accepted)
                    {
                        i++;
                    }
                }
                dp.Value = i;
            }
            return list;
        }
        public List<DataPoint> AddLanguages(List<DataPoint> list)
        {
            List<ShortTourRequest> allRequests = _shortTourRequestRepository.GetAll();
            IEnumerable<string> uniqueLanguages = allRequests.Select(r => r.Language).Distinct();

            foreach (string language in uniqueLanguages)
            {
                if (!list.Any(dp => dp.Key == language))
                {
                    list.Add(new DataPoint() { Key = language, Value = 0 });
                }
            }

            return list;
        }
        public List<DataPoint> FindToursByLocation(int year)
        {
            List<DataPoint> list = new();
            AddLocations(list);
            int i;
            foreach (DataPoint dp in list)
            {
                i = 0;
                foreach (ShortTourRequest shortRequest in _shortTourRequestRepository.GetAll())
                {
                    if ((dp.Key == shortRequest.City) && Convert.ToInt32(year) == shortRequest.From.Year && shortRequest.Status == RequestStatus.Accepted)
                    {
                        i++;
                    }
                }
                dp.Value = i;
            }
            return list;
        }
        public List<DataPoint> FindToursByLocationAllTimes()
        {
            List<DataPoint> list = new();
            AddLocations(list);
            int i;
            foreach (DataPoint dp in list)
            {
                i = 0;
                foreach (ShortTourRequest shortRequest in _shortTourRequestRepository.GetAll())
                {
                    if ((dp.Key == shortRequest.City) && shortRequest.Status == RequestStatus.Accepted)
                    {
                        i++;
                    }
                }
                dp.Value = i;
            }
            return list;
        }
        public List<DataPoint> AddLocations(List<DataPoint> list)
        {
            List<ShortTourRequest> allRequests = _shortTourRequestRepository.GetAll();
            IEnumerable<string> uniqueLocations = allRequests.Select(r => r.City).Distinct();

            foreach (string location in uniqueLocations)
            {
                if (!list.Any(dp => dp.Key == location))
                {
                    list.Add(new DataPoint() { Key = location, Value = 0 });
                }
            }

            return list;
        }
        public double FindAcceptedToursPercentage(int Year)
        {
            double percentage = 0;
            int accepted = 0;
            int sum = 0;
            foreach(ShortTourRequest s in _shortTourRequestRepository.GetAll())
            {
                if(Year == s.From.Year)
                {
                    if (s.Status == RequestStatus.Accepted)
                    {
                        accepted++;
                    }
                    sum++;
                } 
            }
            percentage = 100*(double)accepted/sum;
            return Math.Round(percentage, 1);
        }
        public double FindAcceptedToursPercentageAllTimes()
        {
            double percentage = 0;
            int accepted = 0;
            int sum = 0;
            foreach (ShortTourRequest s in _shortTourRequestRepository.GetAll())
            {
                    if (s.Status == RequestStatus.Accepted)
                    {
                        accepted++;
                    }
                    sum++;
            }
            percentage = 100 * (double)accepted / sum;
            return Math.Round(percentage, 1);
        }
        public double FindAverageInAccepted(int Year)
        {
            int counter = 0;
            int sumPeople = 0;
            foreach (ShortTourRequest s in _shortTourRequestRepository.GetAll())
            {
                if (Year == s.From.Year)
                {
                    if (s.Status == RequestStatus.Accepted)
                    {
                        sumPeople += s.NumberOfPeople;
                        counter++;
                    }
                }
            }
            double percentage = (double)sumPeople / counter;
            return Math.Round(percentage,1);
        }
        public double FindAverageInAcceptedAllTimes()
        {
            int counter = 0;
            int sumPeople = 0;
            foreach (ShortTourRequest s in _shortTourRequestRepository.GetAll())
            {
                    if (s.Status == RequestStatus.Accepted)
                    {
                        sumPeople += s.NumberOfPeople;
                        counter++;
                    }
            }
            double percentage = (double)sumPeople / counter;
            return Math.Round(percentage, 1);
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
