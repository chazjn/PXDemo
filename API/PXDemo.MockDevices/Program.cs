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
var skippedDeviceIds = new List<Guid>();
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
            Console.Write($"{device.Id} {device.Name}: ");
            if(skippedDeviceIds.Contains(device.Id))
            {
                Console.WriteLine("skipped");
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

    Console.WriteLine($"waiting {pollingInterval} seconds, enter an Id to add/remove from skip list");
    var input = await ReadLineWithTimeout(pollingInterval);
    if(Guid.TryParse(input, out Guid result))
    {
        if (!skippedDeviceIds.Remove(result))
            skippedDeviceIds.Add(result);
    }
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

static async Task<string?> ReadLineWithTimeout(int timeoutSeconds)
{
    // Task to read user input asynchronously
    var inputTask = Task.Run(Console.ReadLine);

    // Task to handle the timeout
    var timeoutTask = Task.Delay(TimeSpan.FromSeconds(timeoutSeconds));

    // Wait for the first task to finish
    var completedTask = await Task.WhenAny(inputTask, timeoutTask);

    // Always wait for the full timeout duration
    await timeoutTask;

    // If the input task completes successfully before the timeout, return the result
    if (completedTask == inputTask && inputTask.IsCompletedSuccessfully)
        return inputTask.Result;
    
    return null;
}