import { Directive, inject, OnDestroy, OnInit } from '@angular/core';
import { AppService } from '../services/app.service';

@Directive({
  selector: '[appOnTopDetector]',
})
export class OnTopDetector implements OnInit, OnDestroy {
  private readonly _app = inject(AppService);
  private timeout: any = null;

  ngOnInit(): void {
    this.timeout = setInterval(() => this.updateClass(), 100);
  }

  ngOnDestroy(): void {
    clearInterval(this.timeout);
  }

  private updateClass(): void {
    if (window.scrollY > 150) {
      this._app.isOnTop.set(false);
    } else {
      this._app.isOnTop.set(this._app.dynamicBackground());
    }
  }
}
