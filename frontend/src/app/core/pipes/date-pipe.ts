import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'date',
})
export class DatePipe implements PipeTransform {
  transform(value: unknown, ...args: unknown[]): unknown {
    if (!value) return null;

    if (!(value instanceof Date)) value = new Date(value.toString());

    const date = value as Date;

    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const year = date.getFullYear();

    if (args[0] === 'short') {
      return `${year}-${month}-${day}, ${hours}:${minutes}`;
    } else if (args[0] === 'shortDate') {
      return `${year}-${month}-${day}`;
    }

    return null;
  }
}
