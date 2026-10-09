import { Pipe, PipeTransform } from '@angular/core';

const UNITS: [limit: number, seconds: number, label: string][] = [
  [60, 1, 'sec'],
  [3600, 60, 'min'],
  [86400, 3600, 'hr'],
  [604800, 86400, 'day'],
];

@Pipe({ name: 'timeAgo' })
export class TimeAgo implements PipeTransform {
  transform(value: string): string {
    const seconds = Math.floor((Date.now() - new Date(value).getTime()) / 1000);

    if (seconds < 10) {
      return 'just now';
    }

    for (const [limit, unitSeconds, label] of UNITS) {
      if (seconds < limit) {
        const amount = Math.floor(seconds / unitSeconds);
        return `${amount} ${label}${amount > 1 && label === 'day' ? 's' : ''} ago`;
      }
    }

    return new Date(value).toLocaleDateString('en-GB', {
      day: 'numeric',
      month: 'short',
      year: 'numeric',
    });
  }
}
