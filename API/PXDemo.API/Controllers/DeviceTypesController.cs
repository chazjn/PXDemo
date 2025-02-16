using Microsoft.AspNetCore.Mvc;
using PXDemo.Infrastructure.Models;
using PXDemo.Infrastructure.Services;

namespace PXDemo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceTypesController(IDeviceService deviceService) : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<DeviceType>> Get()
        {
            var types = deviceService.GetDeviceTypes();
            return Ok(types);
        }
    }
}
