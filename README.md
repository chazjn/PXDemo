
# PXDemo
### IoT Devices Status Demo

#### 1. Start the API
- Open /API/PXDemo.sln in Visual Studio
- Start the PXDemo.API project, it should open a browser pointing to url https://localhost:7124/swagger/index.html
- The API will be browsable and testable via Swagger
  
#### 2.Start the UI
- Open /UI/px-ui in Visual Studio Code
- In the console run `ng serve` to start the app
- Browse to url http://localhost:4200/
- A grid showing the pre-seeded IoT data will be displayed

#### 3. Open the Mock Devices App
- In Visual Studio, debug the PXDemo.MockDevices project
- A console window will open and ask for API url and Polling interval, you can leave these as default

#### 4. Using the Mock Devices App
- The app retrieves the list of devices
- On the polling interval there is a 50% chance that signal update will be sent
- Devices that have not sent a signal will be listed as 'SKIPPED'
- The first signal update is a value between 0 and 100
- Subsequent updates will be between -10/+10 of the prior value
- It is possible to 'Pause' updates for devices by copy/pasting the Device Id (one per line) in between polling intervals
- Devices not sending updates will be listed as 'PAUSED'
- Un-pause a device by copy/pasting the device Id again

#### 5. UI Grid
- The grid automatically refreshes every 30 seconds
- The Sort order of the grid is determined by the API








