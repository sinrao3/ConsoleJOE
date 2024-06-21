using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ConsoleAppJOE.Enums;

namespace ConsoleAppJOE.Domain
{
    public class PricePlan
    {
        public string PlanName { get; set; }
        public Supplier EnergySupplier { get; set; }
        public decimal UnitRate { get; set; }
        public decimal GetPrice(DateTime datetime) {
            return UnitRate;
        }
    }

}
