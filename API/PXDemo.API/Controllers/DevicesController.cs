using Microsoft.AspNetCore.Mvc;
using PXDemo.Infrastructure.Dtos;
using PXDemo.Infrastructure.Models;
using PXDemo.Infrastructure.Services;

namespace PXDemo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController(IDeviceService deviceService) : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Device>> Get() 
        {
            var devices = deviceService.GetDevices();

            if (devices == null || devices.Any() == false)
                return Ok(new List<Device>());

            return Ok(devices);
        }

        [HttpGet("{id}")]
        public ActionResult<Device> Get(Guid id) 
        {
            var device = deviceService.GetDeviceById(id);

            if (device == null)
                return NotFound();

            return Ok(device);
        }

        [HttpPost]
        public void Post(DeviceInputDto deviceInput) 
        {
            deviceService.AddDevice(deviceInput);
        }

        [HttpPatch]
        public void Patch(Guid id, [FromBody]DeviceUpdateDto deviceUpdate)
        {
            deviceService.UpdateDevice(id, deviceUpdate);
        }
    }
}
