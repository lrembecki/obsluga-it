import { CompaniesFacade, FormDefinitionsFacade } from '@/app/core';
import { Component, computed, inject } from '@angular/core';
import { Form } from '../../ui/form/form';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.html',
  styleUrl: './contact.scss',
  imports: [Form],
})
export class Contact {
  private readonly _facades = {
    companies: inject(CompaniesFacade),
    formDefinitions: inject(FormDefinitionsFacade),
  };

  protected readonly company = computed(() => this._facades.companies.data()[0]);
  protected readonly formDefinition = computed(() =>
    this._facades.formDefinitions.getByKey('Kontakt'),
  );
  protected readonly keyValues = computed(() =>
    this.company()
      .parameters.slice()
      .sort((a, b) => a.order - b.order),
  );
}
