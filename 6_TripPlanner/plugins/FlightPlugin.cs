// Copyright (c) Microsoft. All rights reserved.
using System.ComponentModel;
using Microsoft.SemanticKernel;
using PersonalPlanner.Models;
using PersonalPlanner.Services;

namespace PersonalPlanner.Plugins;

public class FlightPlugin
{
    [KernelFunction, Description("Rseturn the list of flights available from from City and To City")]
    public static string TravelFromTo(
        [Description("The country from where the user is travelling from")] string travelFromCity,
        [Description("The country to where the user is travelling to")] string travelToCity
    )
    {
        var lst = FlightService.Instance.GetFlights(travelFromCity, travelToCity);

        //iterate over this list and return one consolidated string reading all columns of flight object
        string result = "";
        foreach (var flight in lst)
        {

            result += "Flight Number: " + flight.FlightNumber + " Departure Time: " + flight.DepartureTime + " Arrival Time: " + flight.ArrivalTime + " Price: " + flight.Price + " \n";
        }

        Console.WriteLine($">> Returned list of Flights for {travelFromCity} to {travelToCity}");
        return result;

    }


    #region ...
 /*   [KernelFunction, Description("Take the date and flight number and return the booking details. Don't ask much details just ask the date and flight number and return the booking details.")]
    public static string BookFlight([Description("The date on which the user wants to book the flight")] string date,
        [Description("The flight number which the user wants to book")] string flightNumber)
    {
        Console.WriteLine(">> Flight " + flightNumber + " has been booked on " + date);
        return "Flight " + flightNumber + " has been booked on " + date;
    }
    */

#endregion 

}
