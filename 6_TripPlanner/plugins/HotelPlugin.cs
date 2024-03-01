// Copyright (c) Microsoft. All rights reserved.
using System.ComponentModel;
using Microsoft.SemanticKernel;
using PersonalPlanner.Models;
using PersonalPlanner.Services;

namespace PersonalPlanner.Plugins;

public class HotelPlugin
{
    [KernelFunction, Description("Return the list of Hotels by passing country and city infomration. Don't ask more information, just take the country and city and return the list of hotels.")]
    public static string GetHotels(
        [Description("The country where the user is travelling to")] string country,
        [Description("The city where the user is travelling to")] string city,
        [Description("The Date for which the user wants to book the hotel")] string date
    )
    {
        var lst = HotelService.Instance.GetHotels(country, city, date);
        //iterate over this list and return one consolidated string reading all columns of hotel object
        string result = "";
        foreach (var hotel in lst)
        {
            result += "Hotel Name: " + hotel.Name + " Address: " + hotel.Address + " Price: " + hotel.Price + " \n";
        }

        Console.WriteLine("Returned hotel list of Hotels");

        return result;

    }


    #region ...
/*
    [KernelFunction, Description("Book the hotel by asking From Date and To Date and Hotel Name as parameters. Don't ask more information, just take the fromDate, toDate and hotelName and return the details")]
    public static string BookHotel([Description("The date from which the user wants to book the hotel")] string fromDate,
        [Description("The date till which the user wants to book the hotel")] string toDate,
        [Description("The hotel name which the user wants to book")] string hotelName)
    {
        Console.WriteLine("Hotel " + hotelName + " has been booked from " + fromDate + " to " + toDate);
        return "Hotel " + hotelName + " has been booked from " + fromDate + " to " + toDate;
    }

    */
#endregion
}
