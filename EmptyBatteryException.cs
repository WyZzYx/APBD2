namespace APBD_Project;

public class EmptyBatteryException : Exception
{
    public EmptyBatteryException(string message) : base(message) { }
}

public class EmptySystemException : Exception
{
    public EmptySystemException(string message) : base(message) { }
}

public class ConnectionException : Exception
{
    public ConnectionException(string message) : base(message) { }
}