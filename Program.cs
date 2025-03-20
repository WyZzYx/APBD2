namespace APBD_Project;

using System;

class Program
{
    static void Main()
    {
        string filePath = "/Users/vitaliikoltok/RiderProjects/APBD_Project/APBD_Project/input.txt";
        DeviceManager manager = new DeviceManager(filePath);

        Console.WriteLine("\nDevice Manager");
        Console.WriteLine("1. Add Smartwatch");
        Console.WriteLine("2. Add Personal Computer");
        Console.WriteLine("3. Add Embedded Device");
        Console.WriteLine("4. Remove Device");
        Console.WriteLine("5. Show All Devices");
        Console.WriteLine("6. Turn On Device");
        Console.WriteLine("7. Turn Off Device");
        Console.WriteLine("8. Save & Exit");

        
        
        while (true)
        {
            Console.Write("Choose an option: ");
            string option = Console.ReadLine();

            try
            {
                switch (option)
                {
                    case "1":
                        Console.Write("Enter ID: ");
                        int swId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Name: ");
                        string swName = Console.ReadLine();
                        Console.Write("Enter Battery %: ");
                        int battery = int.Parse(Console.ReadLine());
                        manager.AddDevice(new Smartwatch(swId, swName, true, battery ));
                        break;

                    case "2":
                        Console.Write("Enter ID: ");
                        int pcId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Name: ");
                        string pcName = Console.ReadLine();
                        Console.Write("Enter OS (leave empty if none): ");
                        string os = Console.ReadLine();
                        manager.AddDevice(new PersonalComputer(pcId, pcName, true, os));
                        break;

                    case "3":
                        Console.Write("Enter ID: ");
                        int edId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Name: ");
                        string edName = Console.ReadLine();
                        Console.Write("Enter IP Address: ");
                        string ip = Console.ReadLine();
                        Console.Write("Enter Network Name: ");
                        string network = Console.ReadLine();
                        manager.AddDevice(new EmbeddedDevice(edId, edName, true, ip, network));
                        break;

                    case "4":
                        Console.Write("Enter Device ID to remove: ");
                        int removeId = int.Parse(Console.ReadLine());
                        manager.RemoveDevice(removeId);
                        break;

                    case "5":
                        manager.ShowAllDevices();
                        break;

                    case "6":
                        Console.Write("Enter Device ID to turn on: ");
                        int turnOnId = int.Parse(Console.ReadLine());
                        manager.GetDeviceById(turnOnId)?.TurnOn();
                        break;

                    case "7":
                        Console.Write("Enter Device ID to turn off: ");
                        int turnOffId = int.Parse(Console.ReadLine());
                        manager.GetDeviceById(turnOffId)?.TurnOff();
                        break;

                    case "8":
                        manager.SaveToFile();
                        Console.WriteLine("Data saved. Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid option! Try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
