namespace APBD_Project;

public abstract class Device
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsTurnedOn { get; private set; }
    
    public Device(int id, string name, bool isTurnedOn)
    {
        Id = id;
        Name = name;
        IsTurnedOn = isTurnedOn;
    }

    public virtual void TurnOn()
    {
        IsTurnedOn = true;
    }
    public void TurnOff()
    {
        IsTurnedOn = false;
    }
    public override abstract string ToString();
}