namespace APBD_Project;

public class PersonalComputer : Device
{
    public string OperatingSystem { get; set; }

    public PersonalComputer(int id, string name, bool isTurnedOn, string operatingSystem) : base(id, name, isTurnedOn)
    {
        OperatingSystem = operatingSystem;
    }
    
    public override void TurnOn()
    {
        if (string.IsNullOrEmpty(OperatingSystem))
            throw new EmptySystemException("No operating system installed.");
        
        base.TurnOn();
    }
    
    public override string ToString()
    {
        return $"PC - {Name}, OS: {OperatingSystem}, Turned On: {IsTurnedOn}";
    }
}
