using Microsoft.AspNetCore.Mvc;
using PXDemo.Infrastructure.Models;
using PXDemo.Infrastructure.Services;

namespace PXDemo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController(IDeviceService deviceService) : ControllerBase
    {
        readonly IDeviceService _deviceService = deviceService;

        [HttpGet]
        public ActionResult<IEnumerable<Device>> Get() 
        {
            var devices = _deviceService.GetDevices();

            if (devices == null || devices.Any() == false)
                return Ok(new List<Device>());

            return Ok(devices);
        }

        [HttpGet("{id}")]
        public ActionResult<Device> Get(Guid id) 
        {
            var device = _deviceService.GetDeviceById(id);

            if (device == null)
                return NotFound();

            return Ok(device);
        }

        [HttpPost]
        public void Post(Device device) 
        {
            _deviceService.AddDevice(device);
        }
    }
}
