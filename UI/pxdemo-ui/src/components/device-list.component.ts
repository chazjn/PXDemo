import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeviceService, Device } from '../services/device.service';

@Component({
    selector: 'app-device-list',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './device-list.component.html',
    styleUrls: ['./device-list.component.css']
})
export class DeviceListComponent {
    devices: Device[] = [];

    constructor(private deviceService: DeviceService) {
        this.deviceService.getDevices().subscribe({
            next: data => this.devices = data,
            error: err => console.error('Error fetching devices', err)
        });
    }
}