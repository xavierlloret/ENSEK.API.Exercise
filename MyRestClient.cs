
using RestSharp;
using System;

namespace ENSEK.API.Exercise
{
    public static class MyRestClient 
    {
        public static RestClient newRestClient()
        {
            var options = new RestClientOptions
            {               
                BaseUrl = new Uri("https://qacandidatetest.ensek.io")
            };

            var client = new RestClient(options);
            return client;
        }  
    }
}
