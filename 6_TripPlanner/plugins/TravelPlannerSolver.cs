// Copyright (c) Microsoft. All rights reserved.

using System.ComponentModel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Planning.Handlebars;

namespace PersonalPlanner.Plugins;

public class TravelPlannerSolver
{

    private readonly ILogger _logger;

    public TravelPlannerSolver(ILoggerFactory loggerFactory)
    {
        this._logger = loggerFactory.CreateLogger<TravelPlannerSolver>();
    }

    [KernelFunction]
    [Description("Calls FlightPlugin and HotelPlugin to return list of flights and hotel details")]
    [return: Description("The travel planner to return list of flights and hotel details")]
    public async Task<string> HandleTravelQuery(
        Kernel kernel,
        [Description("return the list of Flights and hotel details available by calling a Flight and Hotel Plugins. Dont ask more details just return the options by calling the function.")] string problem
    )
    {

        var kernelFlightHotel = kernel.Clone();

        // Remove the math solver plugin so that we don't get into an infinite loop
        kernelFlightHotel.Plugins.Remove(kernelFlightHotel.Plugins["TravelPlannerSolver"]);

        // Add the Flight and Hotel plugins so the LLM can solve the problem
        kernelFlightHotel.Plugins.AddFromType<FlightPlugin>();
        kernelFlightHotel.Plugins.AddFromType<HotelPlugin>();

        var planner = new HandlebarsPlanner(new HandlebarsPlannerOptions() { AllowLoops = true });

        // Create a plan
        var plan = await planner.CreatePlanAsync(kernelFlightHotel, problem);
       // Console.WriteLine($"Plan is {plan}"); 
        this._logger.LogInformation($"Plan: {plan}");

        // Execute the plan
        var result = (await plan.InvokeAsync(kernelFlightHotel, [])).Trim();
        this._logger.LogInformation($"Results: {result}");

        return result;
    }
}