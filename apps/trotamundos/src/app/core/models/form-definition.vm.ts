export type FormDefinitionFieldType =
  | 'text'
  | 'email'
  | 'number'
  | 'date'
  | 'date-time'
  | 'textarea'
  | 'select'
  | 'checkbox'
  | 'radio';

export class FormFieldDefinitionVM {
  order: number = 0;
  fieldName: string = null!;
  fieldType: FormDefinitionFieldType = 'text';
  isRequired: boolean = false;
  root: FormDefinitionVM = null!;

  constructor(init?: Partial<FormFieldDefinitionVM>) {
    Object.assign(this, init);
  }
}

export class FormDefinitionVM {
  id: string = null!;
  name: string = null!;
  fields: FormFieldDefinitionVM[] = [];
  notification: Partial<{
    email: { emailId: string; emailTemplateId: string };
  }> = {};
  emailNotificationFieldMapping: {
    emailTemplateFieldName: string;
    formDefinitionFieldName: string;
  }[] = [];

  constructor(init?: Partial<FormDefinitionVM>) {
    Object.assign(this, init);
    this.fields = init?.fields?.map((f) => new FormFieldDefinitionVM(f)) ?? [];

    if (!this.notification) {
      this.notification = {
        email: { emailId: '', emailTemplateId: '' },
      };
    }
  }
}
