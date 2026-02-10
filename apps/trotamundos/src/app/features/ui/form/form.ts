import { FormDefinitionVM } from '@/app/core';
import { Component, computed, input } from '@angular/core';

@Component({
  selector: 'app-form',
  imports: [],
  templateUrl: './form.html',
  styleUrl: './form.scss',
})
export class Form {
  public readonly formDefinition = input.required<FormDefinitionVM>();
  public readonly formItems = computed(() =>
    this.formDefinition()
      .fields.slice()
      .sort((a, b) => a.order - b.order),
  );
}
