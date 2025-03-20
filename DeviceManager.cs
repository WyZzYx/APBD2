namespace APBD_Project;

public class DeviceManager
{
    private readonly List<Device> _devices = new();
    private const int MaxCapacity = 15;
    private readonly string _filePath;

    public DeviceManager(string filePath)
    {
        _filePath = filePath;

        LoadDevices();
    }

    private void LoadDevices()
{
    Console.WriteLine($"Reading file: {_filePath}");

    if (!File.Exists(_filePath))
    {
        Console.WriteLine($"File {_filePath} does not exist.");
        return;
    }

    var lines = File.ReadAllLines(_filePath);
    
    if (lines.Length == 0)
    {
        Console.WriteLine($"File {_filePath} is empty.");
        return;
    }

    int lineNumber = 0;

    foreach (var line in lines)
    {
        lineNumber++;
        Console.WriteLine($"Processing line {lineNumber}: {line}");

        try
        {
            var parts = line.Split(',');
            if (parts.Length < 2) 
            {
                Console.WriteLine($"Skipping invalid line {lineNumber}: {line}");
                continue;
            }

            string[] idParts = parts[0].Split('-');
            if (!int.TryParse(idParts[1], out int id))
            {
                Console.WriteLine($"Invalid ID format in line {lineNumber}: {line}");
                continue;
            }

            string name = parts[1].Trim();
            string deviceType = idParts[0];

            switch (deviceType)
            {
                case "SW":
                    if (parts.Length < 4) continue;  
                    bool isTurnedOn_SW = bool.Parse(parts[2].Trim());
                    int battery = int.Parse(parts[3].Trim().TrimEnd('%'));
                    _devices.Add(new Smartwatch(id, name, isTurnedOn_SW, battery));
                    break;

                case "P":
                    bool isTurnedOn_PC = parts.Length > 2 && bool.TryParse(parts[2].Trim(), out bool result) && result;
                    string os = parts.Length > 3 ? parts[3].Trim() : "";
                    _devices.Add(new PersonalComputer(id, name, isTurnedOn_PC, os));
                    break;

                case "ED":
                    if (parts.Length < 4) continue;  
                    string ip = parts[2].Trim();  
                    string network = parts[3].Trim();
                    _devices.Add(new EmbeddedDevice(id, name, false, ip, network)); 
                    break;

                default:
                    Console.WriteLine($"Unknown device type, skipping: {line}");
                    continue;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing line {lineNumber}: {line}. Exception: {ex.Message}");
        }
    } }




    
    public Device GetDeviceById(int id)
    {
        return _devices.FirstOrDefault(d => d.Id == id);
    }
    public void AddDevice(Device device)
    {
        if (_devices.Count >= MaxCapacity)
            throw new InvalidOperationException("Storage is full.");
        
        _devices.Add(device);
    }
    
    public void RemoveDevice(int id)
    {
        _devices.RemoveAll(d => d.Id == id);
    }
    
    public void ShowAllDevices()
    {
        if (_devices.Count == 0)
        {
            Console.WriteLine("No devices found.");
            return;
        }

        Console.WriteLine("\n--- Device List ---");
        foreach (var device in _devices)
        {
            Console.WriteLine(device.ToString());
        }
        Console.WriteLine($"Total devices: {_devices.Count}");
    }


    
    public void SaveToFile()
    {
        using (StreamWriter writer = new StreamWriter(_filePath))
        {
            foreach (var device in _devices)
            {
                if (device is Smartwatch sw)
                {
                    writer.WriteLine($"SW-{sw.Id},{sw.Name},{sw.IsTurnedOn},{sw.BatteryPercentage}%");
                }
                else if (device is PersonalComputer pc)
                {
                    writer.WriteLine($"P-{pc.Id},{pc.Name},{pc.IsTurnedOn},{pc.OperatingSystem}");
                }
                else if (device is EmbeddedDevice ed)
                {
                    writer.WriteLine($"ED-{ed.Id},{ed.Name},{ed.IPAddress},{ed.NetworkName}");
                }
            }
        }
        Console.WriteLine("Devices successfully saved to file.");
    }

}
