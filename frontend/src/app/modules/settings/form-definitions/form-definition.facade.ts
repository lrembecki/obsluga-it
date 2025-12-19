import { inject } from '@angular/core';
import { ApiFacade } from '@app/core/interfaces/facade.interface';
import {
  ModuleItem,
  NavbarService,
  PageItem,
} from '@app/core/services/navbar.service';
import { FormDefinitionVM } from './form-definition.vm';

export class SettingsFormDefinitionFacade extends ApiFacade<FormDefinitionVM> {
  private readonly _navbar = inject(NavbarService);

  constructor() {
    super([], 'forms/form-definitions');
  }

  protected override withData(data: FormDefinitionVM[]): FormDefinitionVM[] {
    return data
      .map((e) => new FormDefinitionVM(e))
      .sort((a, b) => (a.name || '').localeCompare(b.name || ''))
      .slice();
  }

  override async populate(): Promise<this> {
    const result = await super.populate();

    this._navbar.customModules.set([
      new ModuleItem({
        label: 'Forms',
        items: [
          ...this.data().map(
            (def) =>
              new PageItem({
                label: def.name || 'Unnamed',
                path: 'modules/forms/' + def.id,
              }),
          ),
        ],
      }),
    ]);

    return result;
  }
}
