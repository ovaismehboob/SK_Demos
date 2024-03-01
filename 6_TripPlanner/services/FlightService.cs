using System;
using PersonalPlanner.Models;
using Newtonsoft.Json;

namespace PersonalPlanner.Services
{
    public class FlightService
    {
        private static FlightService _instance;
        public static FlightService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FlightService();
                }
                return _instance;
            }
        }
        public List<Flight> GetFlights(string source, string destination)
        {
            var flights = new List<Flight>
            {
                new Flight
                {
                    Source = source,
                    Destination = destination,
                    FlightNumber = "AI-202",
                    DepartureTime = "10:00 AM",
                    ArrivalTime = "12:00 PM",
                    Duration = "2 hours",
                    Price = "200 USD"
                },
                new Flight
                {
                    Source = source,
                    Destination = destination,
                    FlightNumber = "AI-204",
                    DepartureTime = "12:00 PM",
                    ArrivalTime = "2:00 PM",
                    Duration = "2 hours",
                    Price = "250 USD"
                }
            };



            return flights;

        }
    }
}