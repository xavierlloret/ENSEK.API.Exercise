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
            var authToken = GetToken();
            var response = Requests.ResetTestData(authToken);
            Assert.AreEqual("Success", response);
        }
        [TestMethod]
        public void TestRestClientWithInvalidToken()
        {
            var authToken = "Invalid_Test_Token";
            var response = Requests.ResetTestData(authToken);
            Assert.AreEqual("Unauthorized", response);
        }
    }

    [TestClass]
    public class BuyFuelTests
    {
        [TestMethod]
        public void BuyElectric()
        {
            var response = Requests.PUTBuyEnergyUnits(3,1);
        }
        [TestMethod]
        public void BuyGas()
        {            
            var response = Requests.PUTBuyEnergyUnits(1, 1);
        }
        [TestMethod]
        public void BuyNuclear()
        {
            var response = Requests.PUTBuyEnergyUnits(2, 1);
        }
        [TestMethod]
        public void BuyOil()
        {
            var response = Requests.PUTBuyEnergyUnits(4, 1);
        }

    }
}
