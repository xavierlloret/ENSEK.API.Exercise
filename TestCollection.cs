using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ENSEK.API.Exercise.GetAuthorizationToken;
using RestSharp;
using ENSEK.API.Exercise.Helpers;

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
            int energyType = 3;
            int quantity = 1;
            var response = Requests.PUTBuyEnergyUnits(energyType, quantity);
            string id = Utilities.ExtractOrderId(response);
            var orders = Requests.GETorders();
            Order orderPresent = Utilities.OrderSearch(id, orders);
            Assert.IsNotNull(orderPresent,$"Order with ID '{id}' found in the orders.");
            Assert.AreEqual(1, orderPresent.quantity);
            bool fuelMatch = Utilities.FuelTypeMatch(energyType, orderPresent.fuel);
            Assert.IsTrue(fuelMatch);
        }
        [TestMethod]
        public void BuyGas()
        {
            int energyType = 1;
            int quantity = 1;
            var response = Requests.PUTBuyEnergyUnits(energyType, quantity);
            string id = Utilities.ExtractOrderId(response);
            var orders = Requests.GETorders();
            Order orderPresent = Utilities.OrderSearch(id, orders);
            Assert.IsNotNull(orderPresent, $"Order with ID '{id}' found in the orders.");
            Assert.AreEqual(1, orderPresent.quantity);
            bool fuelMatch = Utilities.FuelTypeMatch(energyType, orderPresent.fuel);
            Assert.IsTrue(fuelMatch);

        }
        [TestMethod]
        public void BuyNuclear()
        {
            int energyType = 2;
            int quantity = 1;
            var response = Requests.PUTBuyEnergyUnits(energyType, quantity);
            Assert.AreEqual("There is no nuclear fuel to purchase!", response);
        }
        [TestMethod]
        public void BuyOil()
        {
            int energyType = 4;
            int quantity = 1;
            var response = Requests.PUTBuyEnergyUnits(energyType, quantity);
            var orders = Requests.GETorders();
            string id = Utilities.ExtractOrderId(response);
            Order orderPresent = Utilities.OrderSearch(id, orders);
            Assert.IsNotNull(orderPresent, $"Order with ID '{id}' found in the orders.");
            Assert.AreEqual(1, orderPresent.quantity);
            bool fuelMatch = Utilities.FuelTypeMatch(energyType, orderPresent.fuel);
            Assert.IsTrue(fuelMatch);
        }

    }
}
