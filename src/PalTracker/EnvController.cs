using Microsoft.AspNetCore.Mvc;

namespace PalTracker {
public class EnvController:ControllerBase
{
    public readonly CloudFoundryInfo CloudFoundryInfo;

    [HttpGet]
    [Route("/env")]
    public CloudFoundryInfo Get() => CloudFoundryInfo;

    public  EnvController(CloudFoundryInfo cloudFoundryInfo)
    {
        CloudFoundryInfo =  cloudFoundryInfo;
    }

}
}