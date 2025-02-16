import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'timeSince',
    pure: false
})
export class TimeSincePipe implements PipeTransform {
    transform(utcDateTime: string | Date): string {
        if (!utcDateTime) return 'Unknown';

        const lastCommunication = new Date(utcDateTime);
        const now = new Date();
        const diffInSeconds = Math.floor((now.getTime() - lastCommunication.getTime()) / 1000);

        if (diffInSeconds < 60) {
            return `${diffInSeconds} sec ago`;
        } else if (diffInSeconds < 3600) {
            const minutes = Math.floor(diffInSeconds / 60);
            const seconds = diffInSeconds % 60;
            return `${minutes} min ${seconds} sec ago`;
        } else {
            const hours = Math.floor(diffInSeconds / 3600);
            const minutes = Math.floor((diffInSeconds % 3600) / 60);
            return `${hours} hr ${minutes} min ago`;
        }
    }
}
