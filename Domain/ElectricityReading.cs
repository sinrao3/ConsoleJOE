using System;
namespace ConsoleAppJOE.Domain
{
    public class ElectricityReading(Decimal reading)
    {
        //public DateTime Time { get; set; }
        public Decimal Reading { get; set; } = reading;
    }

}
