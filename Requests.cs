using Newtonsoft.Json;
using RestSharp;
using System;
using static ENSEK.API.Exercise.MyRestClient;
using static ENSEK.API.Exercise.GetAuthorizationToken;
using Newtonsoft.Json.Linq;

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
    }
}
