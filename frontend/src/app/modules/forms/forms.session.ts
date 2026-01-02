import { computed, inject, signal } from '@angular/core';
import { SettingsFormDefinitionFacade } from '../settings/form-definitions/form-definition.facade';

export class FormSessionService {
  private readonly facades = {
    formDefinitions: inject(SettingsFormDefinitionFacade),
  };
  public readonly formDefinitionId = signal<string | null>(null);
  public readonly formDefinition = computed(() =>
    this.facades.formDefinitions
      .data()
      .find((def) => def.id === this.formDefinitionId()),
  );
}
