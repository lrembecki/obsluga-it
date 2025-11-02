import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { LoadingComponent } from '../ui/loading/loading.component';
import { injectFeatureTemplateServices } from './feature-template.service';

@Component({
  selector: 'app-feature-template',
  imports: [RouterOutlet, LoadingComponent],
  template: `
    @if (loading()) {
      <app-loading />
    }

    @if (initialized()) {
      <router-outlet />
    }
  `,
  styles: `
    :host {
      display: flex;
      flex: auto;
      min-height: 1px;
      position: relative;
      width: 100%;
    }

    ::ng-deep app-feature-template router-outlet {
      display: none;
    }

    ::ng-deep app-feature-template > * {
      flex: auto;
    }
  `,
})
export class FeatureTemplate {
  private readonly _services = injectFeatureTemplateServices();

  protected readonly loading = this._services.template.loading;
  protected readonly initialized = this._services.template.initialized;

  async ngOnInit() {
    await this._services.template.initialize();
  }
}
