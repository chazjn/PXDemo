using Bogus;
using Newtonsoft.Json;
using PXDemo.Infrastructure.Dtos;
using PXDemo.Infrastructure.Models;
using System.Diagnostics;
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
var pausedDeviceId = new List<Guid>();
using var client = new HttpClient();
while (true)
{
	try
	{
        Console.WriteLine();
        var devicesResponse = await client.GetStringAsync($"{rootUrl}/devices/");
        var devices = JsonConvert.DeserializeObject<IEnumerable<Device>>(devicesResponse);

        if (devices == null || devices.Any() == false)
            continue;

        foreach (var device in devices)
        {
            Console.Write($"{device.Id} {device.Name}: ");
            if(pausedDeviceId.Contains(device.Id))
            {
                Console.WriteLine("PAUSED");
                continue;
            }

            //50% chance of signal update
            var update = faker.Random.Number() > 0.5;
            if(update == false)
            {
                Console.WriteLine("SKIPPED");
                continue;
            }

            var deviceUpdateDto = new DeviceUpdateDto
            {
                SignalStrength = GetSignalStrength(device.SignalStrength)
            };

            Console.WriteLine(deviceUpdateDto.SignalStrength);

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

    Console.WriteLine();
    Console.WriteLine($"waiting {pollingInterval} seconds, enter Ids to add/remove from pause list");

    var stopWatch = new Stopwatch();
    stopWatch.Start();

    while(stopWatch.Elapsed.TotalSeconds < pollingInterval)
    {
        if (Console.KeyAvailable)
        {
            var input = Console.ReadLine();
            if (Guid.TryParse(input, out Guid result))
            {
                if (!pausedDeviceId.Remove(result))
                    pausedDeviceId.Add(result);
            }
        }

        Thread.Sleep(100);
    }
    stopWatch.Stop();
}

double GetSignalStrength(double currentSignalStrength)
{
    //unless signal strength is zero it will be determined by previous value
    var updatedSignalStrength = Math.Clamp(
        currentSignalStrength == 0
            ? faker.Random.Number(0, 100) * faker.Random.Double()
            : currentSignalStrength + (faker.Random.Number(-10, 10) * faker.Random.Double()),
        0,
        100);

    return Math.Round(updatedSignalStrength, 3);
}
