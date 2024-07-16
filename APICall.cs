using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DistanceFinder;

public static class OptimalRoute
{
    private static readonly string apiKey = "AIzaSyBT7JvD6dCD0feCYO7lH5Ufq-k0BXI7ubQ"; // Replace with your actual API key

    public static async Task GetDirections()
    {
        List<ClsDistance> distanceLst = [];
        ClsDistance dist;

        //string origin = "41.878113,-87.629799"; // New York, NY
        //string destination = "41.878113,-87.629799"; // New York, NY
        //string waypoints = "29.760427,-95.369804|34.052235,-118.243683"; // Chicago, IL | Houston, TX | Los Angeles, CA

        string origin = "29.6871412,-98.4710214"; 
        string destination = "29.6871412,-98.4710214"; 
        string waypoints = "29.4514403,-98.6821956|29.5124812,-98.5494334|29.5106787,-98.566584999|29.3295016,-98.5742867|29.3720590,-98.388819"; // Chicago, IL | Houston, TX | Los Angeles, CA

        //string requestUrl = $"https://maps.googleapis.com/maps/api/directions/json?origin={origin}&destination={destination}&waypoints={waypoints}&key={apiKey}";
        string requestUrl = $"https://maps.googleapis.com/maps/api/directions/json?origin={origin}&destination={destination}&waypoints=optimize:true|{waypoints}&key={apiKey}";

        using HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        JObject jsonResponse = JObject.Parse(responseBody);

        var routes = jsonResponse["routes"];
        foreach (var route in routes)
        {
            int i = 0;
            var legs = route["legs"];
            foreach (var leg in legs)
            {
                dist = new(){
                    StartAddress = Convert.ToString(leg["start_address"]),
                    EndAddress = Convert.ToString(leg["end_address"]),
                    Distance = Convert.ToString(leg["distance"]["text"]),
                    Duration = Convert.ToString(leg["duration"]["text"])
                };
                i++;
                if (i == 1)
                {
                    Console.WriteLine($"From Address: {dist.StartAddress}  || Customer Address-{i}: {dist.EndAddress}, || Distance: {dist.Distance}, || Duration: {dist.Duration}");
                }
                else
                {
                    Console.WriteLine($"Customer Address - {i-1}: {dist.StartAddress}  || Customer Address-{i}: {dist.EndAddress}, || Distance: {dist.Distance}, || Duration: {dist.Duration}");
                }
                distanceLst.Add(dist);               
                
            }

            
        }
    }
}



