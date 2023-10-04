using System;
using Newtonsoft.Json.Linq;
using RestSharp;
using static ENSEK.API.Exercise.MyRestClient;


namespace ENSEK.API.Exercise
{
    public class GetAuthorizationToken
    {

        public static string GetToken()
        {
            string username = "test";
            string password = "testing";

            var client = newRestClient();

            var request = new RestRequest("/ENSEK/login", Method.Post);
            request.AddBody(new { username = username, password = password });
            

            RestResponse response = client.Execute(request);
            //return response.Content;
            if (!response.IsSuccessful)
            {
                Console.WriteLine("Authorization token request failed with the following error: @{Error}", response.Content);
                throw new Exception(response.Content);
            }
            else
            {
                dynamic responseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(response.Content);
                return responseObj.access_token;
            }
        }

    }
}
