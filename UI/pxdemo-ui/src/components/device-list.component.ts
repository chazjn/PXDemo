import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { interval } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { TimeSincePipe } from "./time-since.pipe";
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-device-list',
    templateUrl: './device-list.component.html',
    styleUrls: ['./device-list.component.css'],
    imports: [CommonModule, TimeSincePipe]
})
export class DeviceListComponent implements OnInit {
    devices: any[] = [];

    constructor(private http: HttpClient) { }

    ngOnInit(): void {
        interval(30000).pipe(
            switchMap(() => this.http.get<any[]>('https://localhost:7124/api/devices'))
        ).subscribe({
            next: data => this.devices = data,
            error: error => console.error('Error fetching devices:', error)
        });

        this.fetchDevices();
    }

    fetchDevices(): void {
        this.http.get<any[]>('https://localhost:7124/api/devices').subscribe({
            next: data => this.devices = data,
            error: error => console.error('Error fetching devices:', error)
        });
    }
}
