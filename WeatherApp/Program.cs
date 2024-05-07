using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            var apiKey = "dd844b9544b7f3ff93b18f6be4849bf9";


            Console.WriteLine("Please write the city name: ");
            var city = Console.ReadLine();

            var response = await httpClient.GetAsync($"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric");
             
            if(response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<WeatherData>(content);

                Console.WriteLine($"Weather forecast for {city} on {data.Main.Temp}°C");
                Console.WriteLine($"Summary: {data.Weather[0].Description}");
            }
            else
            {
                Console.WriteLine($"There is no kind of city like, {city}");
            }

        }
    }

    public class WeatherData
    {
        public Main Main { get; set; }
        public Weather[] Weather { get; set; }
    }

    public class Main
    {
        public double Temp { get; set; }
    }

    public class Weather
    {
        public string Description { get; set; }
    }
}