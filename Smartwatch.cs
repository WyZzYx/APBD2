namespace APBD_Project;

public class Smartwatch : Device, IPowerNotifier
{
    private int _batteryPercentage;
    public int BatteryPercentage 
    {
        get => _batteryPercentage;
        set
        {
            if (value < 0 || value > 100)
                throw new ArgumentException("Battery percentage must be between 0 and 100.");
            
            _batteryPercentage = value;
            if (_batteryPercentage < 20)
                NotifyLowBattery();
        }
    }

    public Smartwatch(int id, string name, bool isTurnedOn, int batteryPercentage) : base(id, name, isTurnedOn)
    {
        BatteryPercentage = batteryPercentage;
    }

    public override void TurnOn()
    {
        if (BatteryPercentage < 11)
            throw new EmptyBatteryException("Battery too low to turn on the device.");
        
        BatteryPercentage -= 10;
        base.TurnOn();
    }
    
    public void NotifyLowBattery()
    {
        Console.WriteLine("Warning: Low Battery!");
    }
    
    public override string ToString()
    {
        return $"Smartwatch - {Name}, Battery: {BatteryPercentage}%, Turned On: {IsTurnedOn}";
    }
}