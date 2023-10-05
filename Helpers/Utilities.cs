using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ENSEK.API.Exercise.Helpers
{
    internal class Utilities
    {
        public static string ExtractOrderId(string message)
        {
            // Define a regular expression pattern for matching GUIDs
            string pattern = @"\b[A-Fa-f0-9]{8}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{12}\b";

            // Find the first match in the input string
            Match match = Regex.Match(message, pattern);
                        
            if (match.Success)
            {               
                string guidString = match.Value;                
                return(guidString);
            }
            else
            {
                return null;
            }
        }
        public static Order OrderSearch(string id, List<Order> orders)
        {
            Order orderFound = null;
            foreach (var item in orders)
            {
                if (item.id == id)
                {
                    orderFound = item;                    
                    break;
                }
            }
            return orderFound;
        }

        public static bool FuelTypeMatch(int numericType, string orderType)
        {
            bool typesMatch= false;
            switch (numericType)
            {
                case 1:
                    if (orderType == "gas")
                    {
                        return typesMatch = true;
                    }
                    break;
                case 2:
                    if (orderType == "nuclear")
                    {
                        return typesMatch = true;
                    }
                    break;
                case 3:
                    if (orderType == "electric")
                    {
                        return typesMatch = true;
                    }
                    break;
                case 4:
                    if (orderType == "Oil")
                    {
                        return typesMatch = true;
                    }                    
                    break;
                      
            }
            return typesMatch;
        }
    }
    
}
