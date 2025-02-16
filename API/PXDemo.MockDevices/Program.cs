using Bogus;
using Newtonsoft.Json;
using PXDemo.Infrastructure.Dtos;
using PXDemo.Infrastructure.Models;
using System.Text;

var rootUrl = "https://localhost:7124/api";
Console.Write($"Api root url [{rootUrl}]: ");
var rootUrlInput = Console.ReadLine();
rootUrl = string.IsNullOrWhiteSpace(rootUrlInput) ? rootUrl : rootUrlInput;

var pollingInterval = 30;
Console.Write($"Polling iterval in seconds [{pollingInterval}]: ");
var pollingIntervalInput = Console.ReadLine();
pollingInterval = string.IsNullOrWhiteSpace(pollingIntervalInput) ? pollingInterval : Convert.ToInt32(pollingIntervalInput);

var faker = new Faker();
using var client = new HttpClient();
while (true)
{
	try
	{
        var devicesResponse = await client.GetStringAsync($"{rootUrl}/devices/");
        var devices = JsonConvert.DeserializeObject<IEnumerable<Device>>(devicesResponse);

        if (devices == null || devices.Any() == false)
            continue;

        foreach (var device in devices)
        {
            var updatedSignalStrength = Math.Clamp(
                device.SignalStrength == 0
                    ? faker.Random.Number(0, 100) * faker.Random.Double()
                    : device.SignalStrength + (faker.Random.Number(-10, 10) * faker.Random.Double()),
                0,
                100);

            var deviceUpdateDto = new DeviceUpdateDto
            {
                SignalStrength = updatedSignalStrength
            };

            Console.WriteLine($"Updating {device.Name}: {updatedSignalStrength}");

            var json = JsonConvert.SerializeObject(deviceUpdateDto);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            await client.PatchAsync($"{rootUrl}/devices/{device.Id}", stringContent);
        }
    }
    catch(Exception ex)
	{
        Console.WriteLine(ex.Message);
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    Console.WriteLine($"waiting {pollingInterval} seconds....");
    Thread.Sleep(pollingInterval * 1000);
}