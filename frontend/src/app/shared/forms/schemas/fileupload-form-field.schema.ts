import { FormFieldSchema } from '../form-field.schema';

export class FileUploadFormFieldSchema<T> extends FormFieldSchema<T> {
  // Accept file MIME types
  accept: string = '*/*';
  // Maximum file size in bytes
  maxFileSize: number = 10000000; // 10MB default
  // Choose button label
  chooseLabel: string = 'Choose File';
  constructor(init?: Partial<FileUploadFormFieldSchema<T>>) {
    super('fileupload', init);
    Object.assign(this, init);
    this.colClass ??= 'col-6';
  }
}
