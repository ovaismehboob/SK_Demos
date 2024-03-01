using System;
using PersonalPlanner.Models;

namespace PersonalPlanner.Services
{
    public class HotelService
    {
        private static HotelService _instance;
        public static HotelService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HotelService();
                }
                return _instance;
            }
        }   
        public List<Hotel> GetHotels(string country, string city, string date)
        {
            return new List<Hotel>
            {
                new Hotel
                {
                    Name = "Hotel A",
                    Address = "Address A",
                    Price = "100 USD",
                    Rating = "4.5",
                    Contact = "1234567890"
                },
                new Hotel
                {
                    Name = "Hotel B",
                    Address = "Address B",
                    Price = "150 USD",
                    Rating = "4.0",
                    Contact = "1234567890"
                }
            };
        }
    }
}