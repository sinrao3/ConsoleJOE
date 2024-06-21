namespace ConsoleAppJOE.Domain
{
    public class MeterReading(string smartMeterId, List<ElectricityReading> electricityReadings)
    {
        public string SmartMeterId { get; set; } = smartMeterId;
        public List<ElectricityReading> ElectricityReadings { get; set; } = electricityReadings;
        
    }
}