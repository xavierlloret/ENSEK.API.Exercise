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
        public static Energy Get_energy()
        {           
            var client = newRestClient();
            var request = new RestRequest("/ENSEK/energy", Method.Get);            

            RestResponse response = client.Execute(request);
            var content = response.Content;

            Energy energy = JsonConvert.DeserializeObject<Energy>(content);            
            return energy;
        }

        public static string Post_Reset(string AuthToken)
        {
            
            var client = newRestClient();
            var request = new RestRequest("/ENSEK/reset", Method.Post);
            request.AddHeader("Authorization", "Bearer " + AuthToken);

            RestResponse response = client.Execute(request);
            dynamic responseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(response.Content);
            return responseObj.message;
        }
        public static string Put_buy(int id,int quantity)
        {

            var client = newRestClient();
            var request = new RestRequest($"/ENSEK/buy/{id}/{quantity}", Method.Put);            

            RestResponse response = client.Execute(request);
            dynamic responseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(response.Content);
            return responseObj.message;
        }

        public static List<Order> Get_Orders()
        {
            var client = newRestClient();
            var request = new RestRequest("/ENSEK/orders", Method.Get);

            RestResponse response = client.Execute(request);
            var content = response.Content;

            List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(content);                        
            return orders;
        }
        public static Order Get_Order(string orderId)
        {
            var client = newRestClient();
            var request = new RestRequest($"/ENSEK/orders/{orderId}", Method.Get);

            RestResponse response = client.Execute(request);
            var content = response.Content;

            Order order = JsonConvert.DeserializeObject<Order>(content);
            return order;
        }

        public static Order Put_Order(string orderId,int quantity, int energy_id)
        {
            var client = newRestClient();
            var request = new RestRequest($"/ENSEK/orders/{orderId}", Method.Put);
            request.AddBody(new { id = orderId, quantity = quantity, energy_id = energy_id });

            RestResponse response = client.Execute(request);
            var content = response.Content;

            Order order = JsonConvert.DeserializeObject<Order>(content);
            return order;
        }

        public static string Delete_Orders(string orderId)
        {
            var client = newRestClient();
            var request = new RestRequest($"/ENSEK/orders/{orderId}", Method.Delete);            

            RestResponse response = client.Execute(request);
            dynamic responseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(response.Content);
            return responseObj.message;
        }
    }
}
