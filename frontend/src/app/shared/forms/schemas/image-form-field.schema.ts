import { FormFieldSchema } from '../form-field.schema';

export class ImageFormFieldSchema<T> extends FormFieldSchema<T> {
  // Accept only image MIME types
  accept: string = 'image/*';
  // Show image preview
  showPreview: boolean = true;
  editable: boolean = false;
  constructor(init?: Partial<ImageFormFieldSchema<T>>) {
    super('image', init);
    Object.assign(this, init);
    this.colClass ??= 'col-6';
  }
}
