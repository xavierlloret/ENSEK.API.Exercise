using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENSEK.API.Exercise.Helpers
{  
    public class Order
    {
        public string fuel { get; set; }
        public string id { get; set; }
        public int quantity { get; set; }
        public string time { get; set; }
    }    
    public class Electric
    {
        public int energy_id { get; set; }
        public double price_per_unit { get; set; }
        public int quantity_of_units { get; set; }
        public string unit_type { get; set; }
    }

    public class Gas
    {
        public int energy_id { get; set; }
        public double price_per_unit { get; set; }
        public int quantity_of_units { get; set; }
        public string unit_type { get; set; }
    }

    public class Nuclear
    {
        public int energy_id { get; set; }
        public double price_per_unit { get; set; }
        public int quantity_of_units { get; set; }
        public string unit_type { get; set; }
    }

    public class Oil
    {
        public int energy_id { get; set; }
        public double price_per_unit { get; set; }
        public int quantity_of_units { get; set; }
        public string unit_type { get; set; }
    }

    public class Energy
    {
        public Electric electric { get; set; }
        public Gas gas { get; set; }
        public Nuclear nuclear { get; set; }
        public Oil oil { get; set; }
    }

    public class Token
    {
        public string access_token { get; set; }
        public string message { get; set; }
    }

}
