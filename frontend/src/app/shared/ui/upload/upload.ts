import { Component, inject, input, output } from '@angular/core';
import { ServiceCallResult } from 'app/core/models/service-call-result.model';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { FileFacade } from 'app/features/files/file.facade';
import { FileModel } from 'app/features/files/file.model';
import { environment } from 'environments/environment';
import { FileUpload, FileUploadEvent } from 'primeng/fileupload';

@Component({
  selector: 'app-upload',
  imports: [FileUpload, TranslatePipe],
  template: `
    <p-fileupload
      mode="basic"
      name="file"
      chooseIcon="pi pi-upload"
      [multiple]="allowMulti()"
      [accept]="accept()"
      [url]="apiEndpoint + endpoint()"
      maxFileSize="100000000"
      (onUpload)="onBasicUploadAuto($event)"
      [auto]="true"
      [chooseLabel]="this.text() | translate"
    />
  `,
  styles: ``,
})
export class Upload {
  public readonly text = input('UI.BROWSE');
  protected readonly apiEndpoint = environment.apiUrl;

  private readonly _facade = inject(FileFacade);

  public readonly endpoint = input.required<string>();
  public readonly accept = input<string>(null!);
  public readonly allowMulti = input(false);
  public readonly uploaded = output<FileModel>();

  async onBasicUploadAuto(event: FileUploadEvent) {
    const result = (
      event.originalEvent as { body: ServiceCallResult<FileModel> }
    ).body;

    if (result.success) {
      await this._facade.populate();
      this.uploaded.emit(result.data!);
    }
  }
}
