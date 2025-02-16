import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeviceListComponent } from '../components/device-list.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, DeviceListComponent],
  templateUrl: './app.component.html',
})
export class AppComponent { }