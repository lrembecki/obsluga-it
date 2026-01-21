import { signal } from '@angular/core';

export class AppService {
  isOnTop = signal<boolean>(false);
  dynamicBackground = signal<boolean>(true);
}
