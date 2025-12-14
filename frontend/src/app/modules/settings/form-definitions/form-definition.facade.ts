import { ApiFacade } from '@app/core/interfaces/facade.interface';
import { FormDefinitionVM } from './form-definition.vm';

export class SettingsFormDefinitionFacade extends ApiFacade<FormDefinitionVM> {
  constructor() {
    super([], 'forms/form-definitions');
  }

  protected override withData(data: FormDefinitionVM[]): FormDefinitionVM[] {
    return data
      .map((e) => new FormDefinitionVM(e))
      .sort((a, b) => (a.name || '').localeCompare(b.name || ''))
      .slice();
  }
}
