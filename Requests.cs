using Newtonsoft.Json;
using RestSharp;
using System;
using static ENSEK.API.Exercise.MyRestClient;
using static ENSEK.API.Exercise.GetAuthorizationToken;
using Newtonsoft.Json.Linq;
using ENSEK.API.Exercise.Helpers;
using System.Collections.Generic;

namespace ENSEK.API.Exercise
{
    public class Requests
    {
        public static RestResponse GetEnergyTypes()
        {           
            var client = newRestClient();
            var request = new RestRequest("/ENSEK/energy", Method.Get);            

            RestResponse response = client.Execute(request);
            var content = response.Content;

            //var requestResponseContent = JsonConvert.DeserializeObject<Json>(content);
            return response;
        }

        public static string ResetTestData(string AuthToken)
        {
            
            var client = newRestClient();
            var request = new RestRequest("/ENSEK/reset", Method.Post);
            request.AddHeader("Authorization", "Bearer " + AuthToken);

            RestResponse response = client.Execute(request);
            dynamic responseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(response.Content);
            return responseObj.message;
        }
        public static string PUTBuyEnergyUnits(int id,int quantity)
        {

            var client = newRestClient();
            var request = new RestRequest($"/ENSEK/buy/{id}/{quantity}", Method.Put);            

            RestResponse response = client.Execute(request);
            dynamic responseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(response.Content);
            return responseObj.message;
        }

        public static List<Order> GETorders()
        {
            var client = newRestClient();
            var request = new RestRequest("/ENSEK/orders", Method.Get);

            RestResponse response = client.Execute(request);
            var content = response.Content;

            List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(content);                        
            return orders;
        }
    }
}
