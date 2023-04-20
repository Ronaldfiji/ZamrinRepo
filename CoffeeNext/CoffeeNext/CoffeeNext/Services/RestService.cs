using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CoffeeNext.Services
{
    public static class RestService
    {
        //public const string GitHubReposEndpoint = "https://api.github.com/orgs/dotnet/repos";

        static string BaseUrl = DeviceInfo.Platform == DevicePlatform.Android ?
                                           "http://10.0.2.2:5000/" : "http://localhost:5000/api";
        private static HttpClient client;
        static RestService()
        {
            try
            {
                client = new HttpClient
                {
                    BaseAddress = new Uri(BaseUrl)

                };
                client.Timeout = TimeSpan.FromSeconds(10);

                if (Device.RuntimePlatform == Device.UWP)
                {
                    client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                }
            }
            catch
            {
                Console.WriteLine("Failed to create api endpoint");
            }
        }

        public static HttpClient getHttpClient()
        {
            return client;
        }

    }
}
