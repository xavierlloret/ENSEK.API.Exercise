using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ENSEK.API.Exercise.GetAuthorizationToken;
using RestSharp;
using ENSEK.API.Exercise.Helpers;
using System.Linq;
using System;
using System.Globalization;
using System.Web;

namespace ENSEK.API.Exercise
{
    [TestClass]
    public class RestClientTests
    {        
        [TestMethod]
        public void TestPost_ResetWithValidToken()
        {            
            var authToken = GetToken();
            var response = Requests.Post_Reset(authToken);
            Assert.AreEqual("Success", response);
        }
        [TestMethod]
        public void TestPost_ResetWithInvalidToken()
        {
            var authToken = "Invalid_Test_Token";
            var response = Requests.Post_Reset(authToken);
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
            var response = Requests.Put_buy(energyType, quantity);
            string id = Utilities.ExtractOrderId(response);
            var orders = Requests.Get_Orders();
            Order orderPresent = Utilities.OrderSearch(id, orders);
            Assert.IsNotNull(orderPresent,$"Order with ID '{id}' found in the orders.");
            Assert.AreEqual(1, orderPresent.quantity);
            bool fuelMatch = Utilities.FuelTypeMatch(energyType, orderPresent.fuel);
            Assert.IsTrue(fuelMatch,"The fuel type does not match the expected value.");
        }
        [TestMethod]
        public void BuyGas()
        {
            int energyType = 1;
            int quantity = 1;
            var response = Requests.Put_buy(energyType, quantity);
            string id = Utilities.ExtractOrderId(response);
            var orders = Requests.Get_Orders();
            Order orderPresent = Utilities.OrderSearch(id, orders);
            Assert.IsNotNull(orderPresent, $"Order with ID '{id}' found in the orders.");
            Assert.AreEqual(1, orderPresent.quantity);
            bool fuelMatch = Utilities.FuelTypeMatch(energyType, orderPresent.fuel);
            Assert.IsTrue(fuelMatch, "The fuel type does not match the expected value.");

        }
        [TestMethod]
        public void BuyNuclear()
        {
            int energyType = 2;
            int quantity = 1;
            var response = Requests.Put_buy(energyType, quantity);
            Assert.AreEqual("There is no nuclear fuel to purchase!", response);
        }
        [TestMethod]
        public void BuyOil()
        {
            int energyType = 4;
            int quantity = 1;
            var response = Requests.Put_buy(energyType, quantity);
            var orders = Requests.Get_Orders();
            string id = Utilities.ExtractOrderId(response);
            Order orderPresent = Utilities.OrderSearch(id, orders);
            Assert.IsNotNull(orderPresent, $"Order with ID '{id}' found in the orders.");
            Assert.AreEqual(1, orderPresent.quantity);
            bool fuelMatch = Utilities.FuelTypeMatch(energyType, orderPresent.fuel);
            Assert.IsTrue(fuelMatch, "The fuel type does not match the expected value.");
        }

    }
    [TestClass]
    public class OrdersTests
    {
        [TestMethod]
        public void TestOrderCountAfterReset()
        {
            Requests.Post_Reset(GetToken());
            var orders = Requests.Get_Orders();            
            DateTime currentDate = DateTime.Now;
            string format = "ddd, dd MMM yyyy HH:mm:ss 'GMT'";
            int ordersBeforeTodayCount = 0;
            string parseErrors = null;
            foreach (var item in orders)
            {

                DateTime orderTime;
                bool parsedSuccessfully = DateTime.TryParseExact(item.time, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out orderTime);
                if (parsedSuccessfully)
                {
                    if (orderTime < currentDate)
                    {
                        ordersBeforeTodayCount++;
                    }                   
                }
                else 
                {
                    parseErrors = " Some order times could not be parsed.";
                }
            }
            int expectedOrderCount = 5;
            Assert.AreEqual (expectedOrderCount, ordersBeforeTodayCount,$"There is a missmatch between the expected and the actual number of valid orders.{parseErrors}");   
            
        }

        [TestMethod]
        public void GetSingleOrder()
        {
            Requests.Post_Reset(GetToken());
            var orders = Requests.Get_Orders();            
            string singleOrderToGet = orders[orders.Count-1].id;
            Order retrievedOrder =  Requests.Get_Order(singleOrderToGet);
            Assert.AreEqual(singleOrderToGet, retrievedOrder.id,"The order retrieved does not match the id requested.");  
        }

        [TestMethod]
        public void UpdateAnOrder()
        {
            Requests.Post_Reset(GetToken());
            var orders = Requests.Get_Orders();

            Order orderdToUpdate = orders[0];
            string orderId = orderdToUpdate.id;
            int newOrderQuantity = 5;
            int newOrderEnergyId = 2;
            Order updatedOrder = Requests.Put_Order(orderId, newOrderQuantity, newOrderEnergyId);
            
            string newOrderEnergyName = Utilities.FuelIntToNameConvert(2);
            Assert.AreEqual(newOrderQuantity, updatedOrder.quantity,"Quantity was not updated correctly.");
            Assert.AreEqual(newOrderEnergyName, updatedOrder.fuel, "Energy was not updated correctly.");
            Assert.IsNotNull(updatedOrder.time,"Time value was not updated correctly");

            //Assert.AreEqual(singleOrderToGet, retrievedOrder.id, "The order retrieved does not match the id requested.");
        }
    }
    [TestClass]
    public class DeleteOrdersTests
    {
        [TestMethod]
        public void DeleteOrder()
        {
            Requests.Post_Reset(GetToken());
            var orders = Requests.Get_Orders();
            string orderToDelete = orders[0].id;
            Requests.Delete_Orders(orderToDelete);

            bool orderPresent = orders.Any(item => item.id == orderToDelete);            
            Assert.IsFalse(orderPresent,$"The order {orderToDelete} has not been deleted correctly.");
        }
    }
    [TestClass]
    public class GetEnergyTests
    {
        [TestMethod]
        public void GetEnergyInfo()
        {
            Energy energyInfo = Requests.Get_energy();
            //in this test all the values could be checked for inconsistencies but without a description of what
            //to check for in each field there is not point.
            Assert.IsTrue(energyInfo.gas.quantity_of_units >= 0);
            Assert.IsTrue(energyInfo.electric.quantity_of_units >= 0);
            Assert.IsTrue(energyInfo.nuclear.quantity_of_units >= 0);
            Assert.IsTrue(energyInfo.oil.quantity_of_units >= 0);
        }

        [TestClass]
        public class LoginTests
        {
            [TestMethod]
            public void LogInWithValidCredentials()
            {
                string username = "test";
                string password = "testing";

                Token loginResponse = Requests.Post_login(username, password);
                Assert.AreEqual("Success", loginResponse.message);
                
            }
            [TestMethod]
            public void LogInWithInvalidUser()
            {
                string username = "invalidtest";
                string password = "testing";

                Token loginResponse = Requests.Post_login(username, password);
                Assert.AreEqual("Unauthorized", loginResponse.message);

            }
            [TestMethod]
            public void LogInWithInvalidPassword()
            {
                string username = "test";
                string password = "invalidtesting";

                Token loginResponse = Requests.Post_login(username, password);
                Assert.AreEqual("Unauthorized", loginResponse.message);
            }
        }
    }
}
