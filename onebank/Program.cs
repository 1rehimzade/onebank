using System;
using System.Collections.Generic;

public interface IDrivable
{
    bool Drive(double kilometers);
}

public interface IRefuellable
{
    bool Refuel(double amount);
}

public interface IVehicle : IDrivable, IRefuellable
{
}

public class Car : IVehicle
{
    public double MileAge { get; private set; }
    public double Fuel { get; private set; }
    public double FuelConsumption { get; private set; }
    public double TankCapacity { get; private set; }

    public Car(double fuel = 20, double tankCapacity = 40, double fuelConsumption = 10)
    {
        Fuel = fuel;
        TankCapacity = tankCapacity;
        FuelConsumption = fuelConsumption;
    }

    public bool Drive(double kilometers)
    {
        double fuelRequired = kilometers / FuelConsumption;
        if (Fuel >= fuelRequired)
        {
            MileAge += kilometers;
            Fuel -= fuelRequired;
            return true;
        }
        return false;
    }

    public bool Refuel(double amount)
    {
        if (amount < 0)
        {
            return false;
        }

        if (Fuel + amount > TankCapacity)
        {
            Fuel = TankCapacity;
        }
        else
        {
            Fuel += amount;
        }

        return true;
    }
}

public class Colored
{
    public static void Write(string text, ConsoleColor color = ConsoleColor.White)
    {
        ConsoleColor originalColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ForegroundColor = originalColor;
    }

    public static void WriteLine(string text, ConsoleColor color = ConsoleColor.White)
    {
        ConsoleColor originalColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = originalColor;
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Colored.Write("Başlayan kimi\n", ConsoleColor.Green);
        Colored.Write("Bakda ne qeder benzin olduğunu daxil edirik: ");
        double initialFuel = double.Parse(Console.ReadLine());

        Colored.Write("Bakın tutumunu daxil edirik: ");
        double tankCapacity = double.Parse(Console.ReadLine());

        Colored.Write("100km-e ne qədər benzin sərf etdiyini daxil edirik: ");
        double fuelConsumption = double.Parse(Console.ReadLine());

        Car car = new Car(initialFuel, tankCapacity, fuelConsumption);

        Dictionary<int, Action> menuActions = new Dictionary<int, Action>
        {
            { 1, () =>
            {
                Colored.Write("Neçe km yol gedəceyinizi daxil edin: ");
                double distance = double.Parse(Console.ReadLine());
                bool success = car.Drive(distance);
                if (success)
                {
                    Colored.WriteLine($"Qalan benzin: {car.Fuel:F2} L, Getdiyiniz yol: {car.MileAge:F2} km", ConsoleColor.Yellow);
                }
                else
                {
                    Colored.WriteLine("Bu yolu getmək mümkün deyil.", ConsoleColor.Red);
                }
            } },
            { 2, () =>
            {
                Colored.Write("Neçe litr benzin dolduracağınızı daxil edin: ");
                double refuelAmount = double.Parse(Console.ReadLine());
                bool success = car.Refuel(refuelAmount);
                if (success)
                {
                    Colored.WriteLine($"Benzin bakı: {car.Fuel:F2} L", ConsoleColor.Yellow);
                }
                else
                {
                    Colored.WriteLine("Səhv daxil etmisiniz.", ConsoleColor.Red);
                }
            } },
            { 3, () => Colored.WriteLine($"Benzin bakı: {car.Fuel:F2} L", ConsoleColor.Yellow) },
            { 4, () => Colored.WriteLine($"Getdiyiniz yol: {car.MileAge:F2} km", ConsoleColor.Yellow) }
        };

        while (true)
        {
            ShowMenu();
            if (int.TryParse(Console.ReadLine(), out int choice) && menuActions.ContainsKey(choice))
            {
                menuActions[choice]();
            }
            else
            {
                Colored.WriteLine("Yanlış seçim.", ConsoleColor.Red);
            }
        }
    }

    static void ShowMenu()
    {
        Colored.WriteLine("\nMenyu:", ConsoleColor.Cyan);
        Colored.WriteLine("1. Sür", ConsoleColor.Cyan);
        Colored.WriteLine("2. Zapravkaya gir", ConsoleColor.Cyan);
        Colored.WriteLine("3. Benzini göstər", ConsoleColor.Cyan);
        Colored.WriteLine("4. Getdiyimiz yolu göstər", ConsoleColor.Cyan);
        Colored.Write("Seçiminizi edin (1-4): ");
    }
}
