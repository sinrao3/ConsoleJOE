
// Build a CLI Application that takes a string as an input, prints it back and again expects another input till
// we exit
using ConsoleAppJOE.Domain;
using ConsoleAppJOE.Enums;
namespace ConsoleAppJOE
{
    class Program
    {
        static Dictionary<string, List<ElectricityReading>> MeterAssociatedReadings = [];
        static List<PricePlan> pricePlans = [];

        public static void Main(string[] args)
        {
            Initialize();
            Selector();
        }

        public Dictionary<string, string> SmartMeterToPricePlanAccounts
        {
            get
            {
                Dictionary<string, string> smartMeterToPricePlanAccounts = new Dictionary<string, string>();
                smartMeterToPricePlanAccounts.Add("smart-meter-0", "price-plan-0");
                smartMeterToPricePlanAccounts.Add("smart-meter-1", "price-plan-2");
                smartMeterToPricePlanAccounts.Add("smart-meter-2", "price-plan-0");
                smartMeterToPricePlanAccounts.Add("smart-meter-3", "price-plan-1");
                smartMeterToPricePlanAccounts.Add("smart-meter-4", "price-plan-2");
                return smartMeterToPricePlanAccounts;
            }
        }

        static void Initialize()
        {

            pricePlans = new List<PricePlan> {
            new PricePlan{
                PlanName = "price-plan-0",
                EnergySupplier = Enums.Supplier.DrEvilsDarkEnergy,
                UnitRate = 10m
            },
            new PricePlan{
                PlanName = "price-plan-1",
                EnergySupplier = Enums.Supplier.TheGreenEco,
                UnitRate = 2m
            },
            new PricePlan{
                PlanName = "price-plan-2",
                EnergySupplier = Enums.Supplier.PowerForEveryone,
                UnitRate = 1m
            }
        };

            List<string> smartMeterIds = ["smart-meter-1", "smart-meter-2", "smart-meter-3", "smart-meter-4", "smart-meter-5"];
            var random = new Random();
            foreach (var smartMeterId in smartMeterIds)
            {
                List<ElectricityReading> electricityReadings = [];
                for (int i = 0; i < 5; i++)
                {
                    var reading = (Decimal)random.NextDouble();
                    ElectricityReading electricityReading = new ElectricityReading(reading);
                    electricityReadings.Add(electricityReading);
                }
                MeterAssociatedReadings.Add(smartMeterId, electricityReadings);
            }
        }

        static void Selector()
        {
            Console.WriteLine("Enter a value:\n 1.Post\n 2.Get\n 3.View Current Price Plan and Compare Usage Cost\n 4.View Recommended Price Plans");
            string option = Console.ReadLine();
            Console.WriteLine("Selected option is:" + option);
            switch (option)
            {
                case "1":
                    storeReadings();
                    break;
                case "2":
                    getReadings();
                    break;
                case "3":
                    viewPricePlanComparison();
                    break;
                case "4":
                    viewRecommendedPlans();
                    break;
            }
            Selector();
        }


        private static void getReadings()
        {
            Console.WriteLine("Please enter smartMeterId");
            string smartMeterId = Console.ReadLine();
            if (MeterAssociatedReadings.ContainsKey(smartMeterId))
            {
                List<ElectricityReading> electricityReadings = MeterAssociatedReadings[smartMeterId];
                electricityReadings.ForEach(electricityReading => { Console.WriteLine(electricityReading.Reading); });
            }
        }

        private static void storeReadings()
        {
            Console.WriteLine("Please enter smartMeterId");
            string smartMeterId = Console.ReadLine();
            Console.WriteLine("Please enter reading value");
            Decimal reading = Decimal.Parse(Console.ReadLine());
            ElectricityReading electricityReading = new ElectricityReading(reading);

            if (!MeterAssociatedReadings.ContainsKey(smartMeterId))
            {
                MeterAssociatedReadings.Add(smartMeterId, new List<ElectricityReading>());
            }

            MeterAssociatedReadings[smartMeterId].Add(electricityReading);
        }

        private static void viewPricePlanComparison()
        {
            Console.WriteLine("Please enter smartMeterId");
            string smartMeterId = Console.ReadLine();
            List<ElectricityReading> electricityReadings = MeterAssociatedReadings[smartMeterId];
            Decimal avgReading = electricityReadings.Sum(e => e.Reading) / electricityReadings.Count;
            pricePlans.ForEach(pricePlan =>
            {
                Decimal pricePlanCost = Math.Round(avgReading * pricePlan.UnitRate, 3);
                Console.WriteLine(pricePlan.EnergySupplier + ": " + pricePlanCost);
            });

        }


        private static void viewRecommendedPlans()
        {
            Console.WriteLine("Please enter smartMeterId");
            string smartMeterId = Console.ReadLine();
            Console.WriteLine("Please enter limit");
            int limit = int.Parse(Console.ReadLine());
            List<ElectricityReading> electricityReadings = MeterAssociatedReadings[smartMeterId];
            Decimal avgReading = electricityReadings.Sum(e => e.Reading) / electricityReadings.Count;
            Dictionary<Supplier, Decimal> keyValuePairs = new Dictionary<Supplier, Decimal>();
            pricePlans.ForEach(pricePlan =>
            {
                Decimal pricePlanCost = Math.Round(avgReading * pricePlan.UnitRate, 3);
                keyValuePairs.Add(pricePlan.EnergySupplier, pricePlanCost);
            });

            var recommendations = keyValuePairs.OrderBy(pricePlanComparison => pricePlanComparison.Value).Take(limit);
            foreach (KeyValuePair<Supplier, Decimal> pair in recommendations)
            {
                Console.WriteLine(pair.Key + ": " + pair.Value + "\n");
            }
        }
    }
}


