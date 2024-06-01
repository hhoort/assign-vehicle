using System;
using System.Collections.Generic;


abstract class ToolVehicle
{
      
    // Static properties
    public static int TotalVehicles ;
    public static int TotalTaxPayingVehicles ;
    public static int TotalNonTaxPayingVehicles ;
    public static decimal TotalTaxCollected ;

    // Instance properties
    public int VehicleID { get; set; }
    public string RegNo { get; set;}
    public string Model { get; set; }
    public string Brand { get; set; }
    public decimal BasePrice { get; set; }
    public string VehicleType { get; set; }

    // Constructor
    public ToolVehicle(int vehicleID, string regNo, string model, string brand, decimal basePrice, string vehicleType)
    {
       this. VehicleID = vehicleID;
        this.RegNo = regNo;
        this.Model = model;
        this.Brand = brand;
        this.BasePrice = basePrice;
        this.VehicleType = vehicleType;
        TotalVehicles++;
    }

    // Abstract method
    public abstract void PayTax();

    // Method for passing without paying
    public void PassWithoutPaying()
    {
        TotalNonTaxPayingVehicles++;
    }
}

class Car : ToolVehicle
{
    public Car(int vehicleID, string regNo, string model, string brand, decimal basePrice)
        : base(vehicleID, regNo, model, brand, basePrice, "Car") { }

    public override void PayTax()
    {
        TotalTaxCollected += 2;
        TotalTaxPayingVehicles++;
    }
}

class Bike : ToolVehicle
{
    public Bike(int vehicleID, string regNo, string model, string brand, decimal basePrice)
        : base(vehicleID, regNo, model, brand, basePrice, "Bike") { }

    public override void PayTax()
    {
        TotalTaxCollected += 1;
        TotalTaxPayingVehicles++;
    }
}

class HeavyVehicle : ToolVehicle
{
    public HeavyVehicle(int vehicleID, string regNo, string model, string brand, decimal basePrice)
        : base(vehicleID, regNo, model, brand, basePrice, "HeavyVehicle") { }

    public override void PayTax()
    {
        TotalTaxCollected += 4;
        TotalTaxPayingVehicles++;
    }
}

class Program
{
    private static int vehicleCounter = 1;
    private static List<ToolVehicle> vehicles = new List<ToolVehicle>();

    private static ToolVehicle CreateVehicle(int type)
    {
        string regNo = "REG" + vehicleCounter;
        string model = "Model" + vehicleCounter;
        string brand = "Brand" + vehicleCounter;
        decimal basePrice = 10000m + (vehicleCounter * 1000);

        ToolVehicle vehicle = null;
        switch (type)
        {
            case 1:
                vehicle = new Car(vehicleCounter, regNo, model, brand, basePrice);
                break;
            case 2:
                vehicle = new Bike(vehicleCounter, regNo, model, brand, basePrice);
                break;
            case 3:
                vehicle = new HeavyVehicle(vehicleCounter, regNo, model, brand, basePrice);
                break;
        }
        vehicleCounter++;
        vehicles.Add(vehicle);
        return vehicle;
    }

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("     Assignment    ");
            Console.WriteLine("Choose vehicle type (1. Car, 2. Bike, 3. Heavy Vehicle, 4. Exit): ");
            string vehicleTypeInput = Console.ReadLine();

            if (vehicleTypeInput == "4")
                break;

            if (!int.TryParse(vehicleTypeInput, out int vehicleType) || vehicleType < 1 || vehicleType > 3)
            {
                Console.WriteLine("Invalid choice. Please try again.");
                continue;
            }

            ToolVehicle vehicle = CreateVehicle(vehicleType);

            Console.WriteLine("Choose action (1. Pay Tax, 2. Pass Without Paying): ");
            string actionInput = Console.ReadLine();

            if (!int.TryParse(actionInput, out int action) || action < 1 || action > 2)
            {
                Console.WriteLine("Invalid action. Please try again.");
                continue;
            }

            switch (action)
            {
                case 1:
                    vehicle.PayTax();
                    break;
                case 2:
                    vehicle.PassWithoutPaying();
                    break;
            }

            Console.WriteLine($"Total Vehicles: {ToolVehicle.TotalVehicles}");
            Console.WriteLine($"Total Tax Paying Vehicles: {ToolVehicle.TotalTaxPayingVehicles}");
            Console.WriteLine($"Total Non-Tax Paying Vehicles: {ToolVehicle.TotalNonTaxPayingVehicles}");
            Console.WriteLine($"Total Tax Collected: {ToolVehicle.TotalTaxCollected}");
        }

        Console.WriteLine("\nSummary of Tax Collection:");
        Console.WriteLine($"Total Vehicles: {ToolVehicle.TotalVehicles}");
        Console.WriteLine($"Total Tax Paying Vehicles: {ToolVehicle.TotalTaxPayingVehicles}");
        Console.WriteLine($"Total Non-Tax Paying Vehicles: {ToolVehicle.TotalNonTaxPayingVehicles}");
        Console.WriteLine($"Total Tax Collected: {ToolVehicle.TotalTaxCollected:C}");
    }
}
