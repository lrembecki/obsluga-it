import { ArrayApiFacade } from '@obsluga-it/core/facades';
import { FormDefinitionVM } from './form-definition.vm';

export class SettingsFormDefinitionFacade extends ArrayApiFacade<FormDefinitionVM> {
  constructor() {
    super([], 'settings/form-definitions');
  }

  protected override withData(data: FormDefinitionVM[]): FormDefinitionVM[] {
    return data.sort((a, b) => (a.name ?? '').localeCompare(b.name ?? ''));
  }
}
