using System;
using System.Collections.Generic;

namespace PersonalPlanner.Models
{
    public class Flight
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }   
    }
}