
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace DistanceFinder;

public class Program
{
    public static void Main(string[] arg)
    {
        Console.WriteLine("Un Optimized Directions");
        OptimalRoute.GetDirections().Wait();
    }
}




