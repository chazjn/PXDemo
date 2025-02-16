import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Device {
    id: string;
    name: string;
    deviceTypeId: number;
    deviceType: {
        id: number;
        name: string;
    } | null;
    isOnline: boolean;
    lastCommunication: Date | null;
    signalStrength: number;
}

@Injectable({
    providedIn: 'root'
})
export class DeviceService {
    private apiUrl = 'https://localhost:7124/api/devices';

    constructor(private http: HttpClient) { }

    getDevices(): Observable<Device[]> {
        return this.http.get<Device[]>(this.apiUrl);
    }
}