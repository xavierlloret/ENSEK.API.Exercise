using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ENSEK.API.Exercise.GetAuthorizationToken;
using RestSharp;



namespace ENSEK.API.Exercise
{
    [TestClass]
    public class RestClientTests
    {        
        [TestMethod]
        public void TestRestClientWithValidToken()
        {
            var client = new RestClient();

            var authToken = GetToken();
            var response = Requests.ResetTestData(authToken);
            Assert.AreEqual("Success", response);
        }
        [TestMethod]
        public void TestRestClientWithInvalidToken()
        {
            var client = new RestClient();

            var authToken = "Invalid_Test_Token";
            var response = Requests.ResetTestData(authToken);
            Assert.AreEqual("Unauthorized", response);
        }
    }
}
