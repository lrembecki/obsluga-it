import {
  EnvironmentProviders,
  inject,
  Provider,
  signal,
  Signal,
} from '@angular/core';
import {
  isLoadingComputed,
  populateFacades,
} from 'app/core/helpers/facade.helper';

class FeatureTemplateService<ProviderSettingsModel> {
  constructor(private readonly _services: ProviderSettingsModel) {
    this.loading = isLoadingComputed(_services);
  }

  private readonly _initialized = signal(false);

  public readonly loading: Signal<boolean> = null!;
  public readonly initialized = this._initialized.asReadonly();

  public async initialize(): Promise<void> {
    await populateFacades(this._services);
    this._initialized.set(true);
  }
}

export function provideFeatureTemplate<ProviderSettingsModel>(
  serviceProvider: () => ProviderSettingsModel,
): (Provider | EnvironmentProviders)[] {
  return [
    {
      provide: FeatureTemplateService,
      useFactory: () => new FeatureTemplateService(serviceProvider()),
    },
  ];
}

export function injectFeatureTemplateServices() {
  return {
    template: inject(FeatureTemplateService),
  };
}
