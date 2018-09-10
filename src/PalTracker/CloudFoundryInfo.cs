
public class CloudFoundryInfo
{
    public string Port;
    public string MemoryLimit;
    public string CfInstanceIndex;
    public string CfInstanceAddr;

    public CloudFoundryInfo(string port, string memoryLimit, string cfInstanceIndex, string cfInstanceAddr)
    {
        Port = port;
        MemoryLimit = memoryLimit;
        CfInstanceIndex = cfInstanceIndex;
        CfInstanceAddr = cfInstanceAddr;
    }

}