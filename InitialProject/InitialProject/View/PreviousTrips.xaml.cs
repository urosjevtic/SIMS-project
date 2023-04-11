//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

//namespace InitialProject.View
//{
//    /// <summary>
//    /// Interaction logic for PreviousTrips.xaml
//    /// </summary>
//    public partial class PreviousTrips : Window
//    {
//        public PreviousTrips()
//        {
//            InitializeComponent();
//        }
//    }
//}







//using initialproject.domain.model;
//using project.controller;
//using project.model;
//using system;
//using system.collections.generic;
//using system.collections.objectmodel;
//using system.linq;
//using system.text;
//using system.threading.tasks;
//using system.windows;
//using system.windows.controls;
//using system.windows.data;
//using system.windows.documents;
//using system.windows.input;
//using system.windows.media;
//using system.windows.media.imaging;
//using system.windows.shapes;
//using system.xml.linq;

//namespace project.view
//{
//    /// <summary>
//    /// interaction logic for reserveview.xaml
//    /// </summary>
//    public partial class reserveview : window
//    {
//        public guest1controller controller { get; set; }
//        public accommodation accommodation { get; set; }

//        int recursion = 0;

//        public observablecollection<accommodationreservation> reservationdates { get; set; }

//        public accommodationreservation selectedreservation { get; set; }
//        public datetime startdate { get; set; } = datetime.now.date;

//        public datetime enddate { get; set; } = datetime.now.date;

//        public reserveview(guest1controller controller, accommodation accommodation)
//        {
//            initializecomponent();
//            datacontext = this;
//            controller = controller;
//            accommodation = accommodation;
//            reservationdates = new observablecollection<accommodationreservation>();

//        }


//        private void dpend_selecteddatechanged(object sender, selectionchangedeventargs e)
//        {
//            if (enddate < datetime.now.date)
//            {
//                string smessageboxtext = $"you have not chosen valid end date!";
//                string scaption = "input error: end date";

//                messageboxbutton btnmessagebox = messageboxbutton.ok;
//                messageboximage icnmessagebox = messageboximage.error;


//                messagebox.show(smessageboxtext, scaption, btnmessagebox, icnmessagebox);
//                dpend.selecteddate = datetime.now.date;
//            }

//        }

//        private void dpstart_selecteddatechanged(object sender, selectionchangedeventargs e)
//        {
//            if (startdate < datetime.now.date)
//            {
//                string smessageboxtext = $"you have not chosen valid start date!";
//                string scaption = "input error: start date";

//                messageboxbutton btnmessagebox = messageboxbutton.ok;
//                messageboximage icnmessagebox = messageboximage.error;


//                messagebox.show(smessageboxtext, scaption, btnmessagebox, icnmessagebox);
//                dpstart.selecteddate = datetime.now.date;
//            }

//        }

//        private void btsearchfreedates_click(object sender, routedeventargs e)
//        {

//            if (!checkconditions()) return;

//            double numofdays = convert.todouble(tbdays.text);

//            double daysbetween = (enddate - startdate).totaldays;


//            reservationdates.clear();

//            while (true)
//            {

//                list<accommodationreservation> reservationsinrange = new list<accommodationreservation>(getreservationsindaterange());

//                var selecteddates = enumerable
//                    .range(0, int.maxvalue)
//                    .select(index => new datetime?(startdate.adddays(index)))
//                    .takewhile(date => date <= enddate)
//                    .todictionary(date => date.value.date, date => true);


//                foreach (var reservation in reservationsinrange)
//                {
//                    var reservationdates = enumerable
//                        .range(0, int.maxvalue)
//                        .select(index => new datetime?(reservation.startdate.adddays(index)))
//                        .takewhile(date => date <= reservation.enddate)
//                        .tolist();

//                    foreach (var date in reservationdates)
//                    {
//                        if (selecteddates.containskey(date.value.date))
//                        {
//                            selecteddates[date.value.date] = false;
//                        }
//                    }

//                }

//                foreach (var date in selecteddates)
//                {
//                    if (date.value == false)
//                    {
//                        continue;
//                    }

//                    if (date.key.adddays(numofdays) > enddate)
//                    {
//                        break;
//                    }

//                    if (selecteddates[date.key.adddays(numofdays)] == false)
//                    {
//                        continue;
//                    }

//                    accommodationreservation reservation =
//                        new(0, date.key, date.key.adddays(numofdays), controller.guest.user.id, accommodation.id);

//                    reservationdates.add(reservation);

//                }

//                if (reservationdates.count == 0)
//                {
//                    startdate = enddate.adddays(1);
//                    enddate = startdate.adddays(daysbetween);
//                    recursion++;

//                }
//                else if (reservationdates.count > 0 && recursion > 0)
//                {
//                    tbnotfound.text = $"we have not been able to find free dates. here are some alternatives in the next {(recursion + 1) * (int)daysbetween} days:";
//                    recursion = 0;
//                    break;
//                }
//                else
//                {
//                    tbnotfound.text = string.empty;
//                    break;
//                }
//            }

//        }

//        private bool isdigitsonly(string str)
//        {
//            return str.all(c => c >= '0' && c <= '9');
//        }

//        private bool isguestsempty()
//        {
//            if (string.isnullorwhitespace(tbguests.text))
//            {
//                string smessageboxtext = $"please enter number of guests.";

//                messageboxbutton btnmessagebox = messageboxbutton.ok;
//                messageboximage icnmessagebox = messageboximage.warning;

//                string scaption = "missing input";
//                messagebox.show(smessageboxtext, scaption, btnmessagebox, icnmessagebox);
//                return true;
//            }
//            return false;
//        }

//        private bool isguestsdigit()
//        {
//            if (!isdigitsonly(tbguests.text))
//            {
//                string smessageboxtext = $"number of guests field must contain only digits!";

//                messageboxbutton btnmessagebox = messageboxbutton.ok;
//                messageboximage icnmessagebox = messageboximage.error;

//                string scaption = "input error: number of guests";
//                messagebox.show(smessageboxtext, scaption, btnmessagebox, icnmessagebox);
//                return false;
//            }

//            return true;
//        }

//        private bool isdaysempty()
//        {
//            if (string.isnullorwhitespace(tbdays.text))
//            {
//                string smessageboxtext = $"please enter number of days.";

//                messageboxbutton btnmessagebox = messageboxbutton.ok;
//                messageboximage icnmessagebox = messageboximage.warning;

//                string scaption = "missing input";
//                messagebox.show(smessageboxtext, scaption, btnmessagebox, icnmessagebox);
//                return true;
//            }
//            return false;
//        }

//        private bool isdaysdigit()
//        {
//            if (!isdigitsonly(tbdays.text))
//            {
//                string smessageboxtext = $"number of days field must contain only digits!";

//                messageboxbutton btnmessagebox = messageboxbutton.ok;
//                messageboximage icnmessagebox = messageboximage.error;

//                string scaption = "input error: number of days";
//                messagebox.show(smessageboxtext, scaption, btnmessagebox, icnmessagebox);
//                return false;
//            }

//            return true;
//        }

//        private bool checkmaxguestslimit(int numofguests)
//        {
//            if (numofguests > accommodation.maxguests)
//            {
//                string smessageboxtext = $"maximum number of guests for this accommodation is {accommodation.maxguests}!";

//                messageboxbutton btnmessagebox = messageboxbutton.ok;
//                messageboximage icnmessagebox = messageboximage.error;

//                string scaption = "exceeded number of guests";
//                messagebox.show(smessageboxtext, scaption, btnmessagebox, icnmessagebox);
//                return false;
//            }

//            return true;
//        }

//        private bool checkminreservationlimit(int numofdays)
//        {
//            if (numofdays < accommodation.minreservationdays)
//            {
//                string smessageboxtext = $"minimal reservation for this accommodation is {accommodation.minreservationdays} days!";

//                messageboxbutton btnmessagebox = messageboxbutton.ok;
//                messageboximage icnmessagebox = messageboximage.error;

//                string scaption = "minimal reservation limit";
//                messagebox.show(smessageboxtext, scaption, btnmessagebox, icnmessagebox);
//                return false;
//            }

//            return true;
//        }

//        private bool isendbeforestart()
//        {
//            if (startdate.date > enddate.date)
//            {
//                string smessageboxtext = $"start date cannot be before end date!";
//                string scaption = "date not valid";

//                messageboxbutton btnmessagebox = messageboxbutton.ok;
//                messageboximage icnmessagebox = messageboximage.error;


//                messagebox.show(smessageboxtext, scaption, btnmessagebox, icnmessagebox);
//                dpstart.selecteddate = enddate;

//                return true;
//            }

//            return false;
//        }

//        private bool isenddatevalid(double numofdays)
//        {
//            if (startdate.date.adddays(numofdays) > enddate.date)
//            {
//                string smessageboxtext = $"chosen start and end date does not match entered numer of days!";
//                string scaption = "date not valid";

//                messageboxbutton btnmessagebox = messageboxbutton.ok;
//                messageboximage icnmessagebox = messageboximage.error;


//                messagebox.show(smessageboxtext, scaption, btnmessagebox, icnmessagebox);
//                dpstart.selecteddate = enddate;

//                return false;
//            }

//            return true;
//        }

//        private bool checkconditions()
//        {
//            if (isguestsempty()) return false;

//            if (isdaysempty()) return false;

//            if (!isguestsdigit()) return false;

//            if (!isdaysdigit()) return false;

//            int numofguests = convert.toint32(tbguests.text);

//            if (!checkmaxguestslimit(numofguests)) return false;

//            double numofdays = convert.todouble(tbdays.text);

//            if (!checkminreservationlimit((int)numofdays)) return false;


//            // date check
//            if (isendbeforestart()) return false;

//            if (!isenddatevalid(numofdays)) return false;

//            return true;
//        }

//        private list<accommodationreservation> getreservationsindaterange()
//        {
//            list<accommodationreservation> reservations = new list<accommodationreservation>(controller.getallreservations());
//            list<accommodationreservation> reservationsinrange = new list<accommodationreservation>();

//            foreach (var reservation in reservations)
//            {
//                if (reservation.accommodationid == accommodation.id)
//                {
//                    if ((reservation.startdate > enddate) || (reservation.enddate < startdate))
//                        continue;

//                    reservationsinrange.add(reservation);
//                }


//            }

//            return reservationsinrange;
//        }

//        private void btreserve_click(object sender, routedeventargs e)
//        {
//            if (selectedreservation == null)
//            {
//                string smessageboxtext = $"choose a reservation first!";
//                string scaption = "reservation not chosen";

//                messageboxbutton btnmessagebox = messageboxbutton.ok;
//                messageboximage icnmessagebox = messageboximage.warning;


//                messagebox.show(smessageboxtext, scaption, btnmessagebox, icnmessagebox);
//                return;
//            }

//            var reservation = controller.guest.reservations.find(r => (r.accommodationid == selectedreservation.accommodationid) &&
//                                                    (r.startdate == selectedreservation.startdate) &&
//                                                    (r.enddate == selectedreservation.enddate));
//            if (reservation != null)
//            {
//                string smessageboxtext = $"you have already made this reservation!";
//                string scaption = "reservation already exists";

//                messageboxbutton btnmessagebox = messageboxbutton.ok;
//                messageboximage icnmessagebox = messageboximage.error;


//                messagebox.show(smessageboxtext, scaption, btnmessagebox, icnmessagebox);
//                return;
//            }

//            messageboxresult result = messagebox.show("are you sure you want to reserve this accommodation at chosen date?", "confirm reservation",
//                    messageboxbutton.yesno, messageboximage.question);

//            if (result == messageboxresult.yes)
//            {
//                selectedreservation.guest = controller.guest;
//                selectedreservation.accommodation = accommodation;
//                controller.addreservation(selectedreservation);

//            }

//        }
//    }
//}
