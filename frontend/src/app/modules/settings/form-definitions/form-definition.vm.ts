export type FormDefinitionFieldType =
  | 'text'
  | 'email'
  | 'number'
  | 'date'
  | 'textarea'
  | 'select'
  | 'checkbox'
  | 'radio';

export class FormFieldDefinitionVM {
  order: number = 0;
  fieldName: string = null!;
  fieldType: FormDefinitionFieldType = 'text';
  isRequired: boolean = false;

  constructor(init?: Partial<FormFieldDefinitionVM>) {
    Object.assign(this, init);
  }
}

export class FormDefinitionVM {
  id: string = null!;
  name: string = null!;
  fields: FormFieldDefinitionVM[] = [];

  constructor(init?: Partial<FormDefinitionVM>) {
    Object.assign(this, init);
    this.fields = init?.fields?.map((f) => new FormFieldDefinitionVM(f)) ?? [];
  }
}
