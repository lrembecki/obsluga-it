import { StorageVM } from '@app/shared/storage/storage.vm';

export class EmailTemplateVM {
  id: string = null!;
  name: string = '';
  templateHtml: StorageVM = new StorageVM();
  fields: string[] = [];

  constructor(init?: Partial<EmailTemplateVM>) {
    Object.assign(this, init);
    if (init?.templateHtml) {
      this.templateHtml = new StorageVM(init.templateHtml);
    }
  }
}
