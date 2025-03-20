using System.Text.RegularExpressions;

namespace APBD_Project;

public class EmbeddedDevice : Device
{
    private static readonly Regex IpRegex = new Regex(@"^\d{1,3}(\.\d{1,3}){3}$");
    public string IPAddress { get; set; }
    public string NetworkName { get; set; }

    public EmbeddedDevice(int id, string name, bool isTurnedOn, string ipAddress, string networkName) : base(id, name, isTurnedOn)
    {
        if (!IpRegex.IsMatch(ipAddress))
            throw new ArgumentException("Invalid IP Address format.");
        
        IPAddress = ipAddress;
        NetworkName = networkName;
    }
    
    public void Connect()
    {
        if (!NetworkName.Contains("MD Ltd."))
            throw new ConnectionException("Cannot connect to this network.");
    }
    
    public override void TurnOn()
    {
        Connect();
        base.TurnOn();
    }
    
    public override string ToString()
    {
        return $"Embedded Device - {Name}, IP: {IPAddress}, Network: {NetworkName}, Turned On: {IsTurnedOn}";
    }
}